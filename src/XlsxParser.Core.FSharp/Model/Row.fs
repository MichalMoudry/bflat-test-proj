namespace XlsxParser.Core.FSharp.Model

/// A class representing a single cell in an Excel file.
[<Sealed>]
type Cell(value: obj) =
    member this.Value with get() = value
    member this.Typeof with get() = value.GetType()

type Row = {
    Cells: array<Cell>
}
