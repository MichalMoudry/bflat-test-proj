namespace XlsxParser.Core.FSharp.Model

open System

/// A record representing a single cell in an Excel file.
[<Struct>]
type Cell = {
    Value: obj
    Typeof: Type
}

type Row = {
    Cells: array<Cell>
}
