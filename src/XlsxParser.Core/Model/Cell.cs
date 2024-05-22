using System.Globalization;

namespace XlsxParser.Core.Model;

/// <summary>
/// A structure representing a singular cell in a Excel file.
/// </summary>
internal readonly record struct Cell(
    string? ColumnName,
    Type DataType,
    string Value
)
{
    public object GetValue() => Convert.ChangeType(
        Value,
        DataType,
        CultureInfo.InvariantCulture
    );
}