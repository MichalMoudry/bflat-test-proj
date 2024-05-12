using System.Reflection;
using ClosedXML.Excel;
using XmlParser.Core.Errors;

namespace XmlParser.Core;

public sealed class ReaderService
{
    public static (ICollection<T>, string?) ParseWorkbook<T>(string file) where T : new()
    {
        ArgumentException.ThrowIfNullOrEmpty(file);
        var propertyData = typeof(T).GetProperties().ToArray();

        using var workbook = new XLWorkbook(file);
        var worksheet = workbook.Worksheets.First();
        var numberOfDataRows = worksheet.RowsUsed().Count() - 1;
        var res = new List<T>(numberOfDataRows);

        var isFirstRowValid = worksheet
            .Row(1).CellsUsed()
            .Select(i => i.CachedValue.GetText().ToLowerInvariant())
            .SequenceEqual(propertyData.Select(i => i.Name.ToLowerInvariant()));
        // If the first row is not structurally equal to T properties, then early return.
        if (!isFirstRowValid)
        {
            return (Array.Empty<T>(), Errs.InvalidFirstRow);
        }

        // Adding 2 because index starts at 1 and header row is not evaluated
        for (var i = 2; i < numberOfDataRows + 2; i++)
        {
            res.Add(new T());
        }
        return (res, null);
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