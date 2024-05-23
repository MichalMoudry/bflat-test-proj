namespace XlsxParser.Core.FSharp.Model

open XlsxParser.Core.Attributes

/// A record encapsulating relevant property information for file processing.
type internal PropertyDetail(name : string, columnAttr : ColumnNameAttribute) =
    /// Name of the property.
    member this.Name with get() = name
    /// Name of a column in an Excel file that the property represents.
    member this.ColumnName with get() =
        if columnAttr <> null then
            Some(columnAttr.Name)
        else
            None
