namespace XmlParser.Core.Model;

/// <summary>
/// A structure representing a singular cell in a Excel file.
/// </summary>
internal readonly record struct Cell(
    string? ColumnName,
    Type DataType,
    string Value
);