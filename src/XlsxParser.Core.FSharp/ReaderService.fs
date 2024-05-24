namespace XlsxParser.Core.FSharp

open System
open System.IO
open System.Linq
open System.Reflection
open FSharp.Interop.Excel
open XlsxParser.Core.Attributes
open XlsxParser.Core.FSharp.Model

[<Sealed>]
type ReaderService() =
    let GetColumnNames(evalType : Type) =
        evalType
            .GetProperties()
            .Select(fun i -> PropertyDetail(i.Name, i.GetCustomAttribute<ColumnNameAttribute>()))
            .Where(fun i -> i.ColumnName.IsSome)
            .ToDictionary(fun k -> k.Name)

    member this.ParseWorkbook<'T> (file: Stream) format sheetName range =
        seq {
            if file.Length = 0 then ()
            else
                let columnNamesCount = GetColumnNames(typeof<'T>).Count - 1
                if columnNamesCount <= 0 then ()
                else
                    let worksheet = ExcelProvider.ExcelFileInternal(file, format, sheetName, range, true)
                    let numberOfRows = (worksheet.Data |> Seq.length) - 1
                    for i in 0..numberOfRows do
                        let rowData = worksheet.Data.ElementAt(i)
                        yield! seq {
                            { Cells = [| for y in 0..columnNamesCount -> Cell(rowData.GetValue y) |] }
                        }
        }
