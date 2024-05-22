namespace XlsxParser.Core.FSharp.Attributes

open System

/// An attribute for assigning class properties to Excel column.
[<Sealed>]
type ColumnNameAttribute(name: string) =
    inherit Attribute()
    /// Name of the column in the Excel file.
    member this.Name with get () = name
