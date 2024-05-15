using System.Globalization;
using System.Reflection;
using ClosedXML.Excel;
using XmlParser.Core.Attributes;
using XmlParser.Core.Errors;
using XmlParser.Core.Model;

namespace XmlParser.Core;

public sealed class ReaderService
{
    public static ParsingResult<T> ParseWorkbook<T>(string file) where T : new()
    {
        ArgumentException.ThrowIfNullOrEmpty(file);
        var propertyData = typeof(T).GetProperties().ToArray();
        var columnNames = propertyData
            .Select(i => i.GetCustomAttribute<ColumnNameAttribute>()?.Name)
            .ToArray();
        if (propertyData.Length == 0 || columnNames.Length == 0)
        {
            return new ParsingResult<T>(Array.Empty<T>(), Errs.ZeroProperties);
        }

        using var workbook = new XLWorkbook(file);
        var worksheet = workbook.Worksheets.First();
        var numberOfDataRows = worksheet.RowsUsed().Count() - 1;
        var res = new List<T>(numberOfDataRows);

        var isFirstRowValid = worksheet
            .Row(1).CellsUsed()
            .Select(i => i.CachedValue.GetText().ToLowerInvariant())
            .SequenceEqual(columnNames.Select(i => i?.ToLowerInvariant()));
        // If the first row is not structurally equal to T properties, then early return.
        if (!isFirstRowValid)
        {
            return new ParsingResult<T>(Array.Empty<T>(), Errs.InvalidFirstRow);
        }

        // Adding 2 because index starts at 1 and header row is not evaluated
        for (var i = 2; i < numberOfDataRows + 2; i++)
        {
            var obj = new T();
            var cells = worksheet
                .Row(i)
                .CellsUsed()
                .Take(columnNames.Length)
                .Select((c, v) => new Cell(
                    MatchIndexToColumnName(v, columnNames!),
                    MatchExcelTypeToDotnet(c.DataType),
                    c.CachedValue.ToString(CultureInfo.InvariantCulture)
                ));
            foreach (var cell in cells)
            {
                var setPropResult = TrySetProperty(
                    obj,
                    cell.ColumnName,
                    Convert.ChangeType(cell.Value, cell.DataType, CultureInfo.InvariantCulture)
                );
                if (!setPropResult)
                {
                    return new ParsingResult<T>(
                        Array.Empty<T>(),
                        string.Format(null, Errs.InvalidCell, cell.Value, cell.ColumnName)
                    );
                }
            }
            res.Add(obj);
        }
        return new ParsingResult<T>(res);
    }

    /// <summary>
    /// A structure representing a singular cell in a Excel file.
    /// </summary>
    private readonly record struct Cell(
        string ColumnName,
        Type DataType,
        string Value
    );

    private static string MatchIndexToColumnName(int index, string[] columnNames) =>
        index >= 0 && index < columnNames.Length
            ? columnNames[index]
            : throw new ArgumentOutOfRangeException(nameof(index));

    /// <summary>
    /// Method for matching an Excel type to a corresponding .NET type.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when there is an attempt to match currently unsupported Excel data type.
    /// </exception>
    private static Type MatchExcelTypeToDotnet(XLDataType sourceType)
    {
        return sourceType switch
        {
            XLDataType.Text => typeof(string),
            XLDataType.Number => typeof(int),
            XLDataType.Boolean => typeof(bool),
            XLDataType.DateTime => typeof(DateTime),
            _ => throw new ArgumentOutOfRangeException(
                nameof(sourceType),
                $"{sourceType} is not currently supported"
            )
        };
    }

    private static bool TrySetProperty(object obj, string property, object value)
    {
        var prop = obj
            .GetType()
            .GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
        if (prop == null || !prop.CanWrite) return false;
        prop.SetValue(obj, value, null);
        return true;
    }
}