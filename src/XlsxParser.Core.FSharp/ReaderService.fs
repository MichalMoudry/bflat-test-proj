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

    member this.ParseWorkbook<'T> (file: Stream) format sheetName =
        seq {
            if file.Length = 0 then
                ()
            else
                let columnNames = GetColumnNames(typeof<'T>)
                if columnNames.Count = 0 then
                    ()
                else
                    let worksheet = ExcelProvider.ExcelFileInternal(file, format, sheetName, "A1:F3", true)
                    let numberOfRows = (worksheet.Data |> Seq.length) - 1
                    for i in 0..numberOfRows do
                        let rowData = worksheet.Data.ElementAt(i)
                        yield! seq { { Cells = [||] } }
                        (*for y in 0..columnNames.Count - 1 do
                            let value = (rowData.GetValue y).ToString()
                            printfn ""
                        yield! seq { { Cells = [||] } }*)
        }
