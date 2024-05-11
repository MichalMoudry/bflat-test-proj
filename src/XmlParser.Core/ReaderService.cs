using ClosedXML.Excel;
using XmlParser.Core.Errors;
using XmlParser.Core.Model;

namespace XmlParser.Core;

public sealed class ReaderService
{
    public static (ICollection<T>, string?) ParseWorkbook<T>(string file) where T : new()
    {
        ArgumentException.ThrowIfNullOrEmpty(file);
        var propertyData = typeof(T)
            .GetProperties()
            .Select(i => new PropertyData(
                i.Name.ToLowerInvariant(),
                i.PropertyType
            ))
            .ToArray();

        using var workbook = new XLWorkbook(file);
        var worksheet = workbook.Worksheets.First();
        var numberOfDataRows = worksheet.RowsUsed().Count() - 1;
        var res = new List<T>(numberOfDataRows);

        var firstRow = worksheet
            .Row(1).CellsUsed()
            .Select(i => i.CachedValue.GetText().ToLowerInvariant());
        // If the first row is not structurally equal to T properties, then early return.
        if (!propertyData.Select(i => i.PropertyName).SequenceEqual(firstRow))
        {
            return (Array.Empty<T>(), Errs.InvalidFirstRow);
        }

        for (var i = 0; i < numberOfDataRows; i++)
        {
            res.Add(new T());
        }
        return (res, null);
    }
}