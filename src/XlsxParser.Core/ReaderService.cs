using System.Globalization;
using System.Reflection;
using ClosedXML.Excel;
using XlsxParser.Core.Api;
using XlsxParser.Core.Attributes;
using XlsxParser.Core.Errors;
using XlsxParser.Core.Model;

namespace XlsxParser.Core;

/// <inheritdoc/>
public sealed class ReaderService : IReaderService
{
    /// <inheritdoc/>
    public IEnumerable<Row> ParseWorkbook<T>(Stream file)
    {
        if (file.Length == 0)
        {
            yield break;
        }
        var columnNames = GetColumnNames(typeof(T));
        if (columnNames.Count == 0)
        {
            yield break;
        }

        using var workbook = new XLWorkbook(file);
        var worksheet = workbook.Worksheets.First();
        var numberOfDataRows = worksheet.RowsUsed().Count() - 1;

        // If the first row is not structurally equal to T properties, then early return.
        if (!IsRowStructureValid(worksheet.Row(1).CellsUsed(), columnNames))
        {
            yield break;
        }

        // Adding 2 because index starts at 1 and header row is not evaluated
        for (var i = 2; i < numberOfDataRows + 2; i++)
        {
            yield return new Row(worksheet
                .Row(i)
                .CellsUsed()
                .Select(uc => new Cell
                {
                    Value = uc.CachedValue.ToString(CultureInfo.InvariantCulture),
                    Typeof = MatchExcelTypeToDotnet(uc.DataType)
                })
                .ToArray());
        }
    }

    /// <summary>
    /// Method for obtaining a dictionary of pairs between a property name and its column name attribute value.
    /// </summary>
    private static Dictionary<string, string> GetColumnNames(Type type) => type
        .GetProperties()
        .Select(i => new PropertyDetail(
            i.Name,
            i.GetCustomAttribute<ColumnNameAttribute>()?.Name
        ))
        .Where(i => i.ColumnName != null)
        .ToDictionary(k => k.Name, v => v.ColumnName!);

    private static bool IsRowStructureValid(
        IXLCells cells,
        Dictionary<string, string> structureInfo
    ) => cells
        .Select(i => i.CachedValue.GetText().ToLowerInvariant())
        .SequenceEqual(structureInfo.Select(i => i.Value.ToLowerInvariant()));

    /// <summary>
    /// Method for matching a numerical index to type's property name.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="index"/> is out of range of type's properties.
    /// </exception>
    private static string MatchIndexToPropertyName(
        int index,
        Dictionary<string, string> columns
    ) => index >= 0 && index < columns.Count
        ? columns.ElementAt(index).Key
        : throw new ArgumentOutOfRangeException(nameof(index));

    /// <summary>
    /// Method for matching an Excel type to a corresponding .NET type.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when there is an attempt to match currently unsupported Excel data type.
    /// </exception>
    private static Type MatchExcelTypeToDotnet(XLDataType sourceType) => sourceType switch
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