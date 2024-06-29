using System.Globalization;

namespace ExcelService.Export;

/// <summary>
/// A class responsible for exporting data into an Excel file.
/// </summary>
public interface IExcelExporter
{
    /// <summary>
    /// A method for exporting a sequence of items into an Excel file.
    /// </summary>
    MemoryStream ExportData<T>(
        IEnumerable<T> data,
        string sheetName = "Sheet 1",
        CultureInfo? cultureInfo = null
    );

    /// <summary>
    /// A method for exporting a collection of items into an Excel file.
    /// </summary>
    MemoryStream ExportData<T>(
        ICollection<T> data,
        string sheetName = "Sheet 1",
        CultureInfo? cultureInfo = null
    );
}