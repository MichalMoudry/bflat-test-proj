namespace XlsxParser.Core.Model;

/// <summary>
/// A record encapsulating relevant property information for file processing.
/// </summary>
/// <param name="Name">Name of the property.</param>
/// <param name="ColumnName">
/// Name of a column in an Excel file that the property represents.
/// </param>
internal sealed record PropertyDetail(
    string Name,
    string? ColumnName
);