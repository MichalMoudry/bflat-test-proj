namespace XlsxParser.Core.FSharp

open System
open System.Linq
open System.IO
open FSharp.Interop.Excel

[<Sealed>]
type ReaderService() =
    let GetColumnNames(evalType : Type) =
        evalType.GetProperties().Select(fun i -> {||})
        ()

    member this.ParseWorkbook<'T>(file: Stream, format, sheetName) =
        let worksheet = ExcelProvider.ExcelFileInternal(file, format, sheetName, "", true)
        let test = (worksheet.Data |> Seq.head).ToString()
        printfn $"%s{test}"
        ()
