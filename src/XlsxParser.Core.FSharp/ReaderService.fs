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
            .Select(fun i -> { Name = i.Name; ColumnName = i.GetCustomAttribute<ColumnNameAttribute>().Name })
            .Where(fun i -> i.ColumnName <> String.Empty)
            .ToDictionary(fun k -> k.Name)

    member this.ParseWorkbook<'T> (file: Stream) format sheetName =
        if file.Length = 0 then
            Seq.empty<'T>
        else
            let worksheet = ExcelProvider.ExcelFileInternal(file, format, sheetName, "", true)
            let test = (worksheet.Data |> Seq.head).ToString()
            printfn $"%s{test}"
            Seq.empty<'T>
