namespace XlsxParser.Core.FSharp.Model

open System

/// A record encapsulating relevant property information for file processing.
type internal PropertyDetail = {
    /// Name of the property.
    Name : string
    /// Name of a column in an Excel file that the property represents.
    ColumnName : string
}
