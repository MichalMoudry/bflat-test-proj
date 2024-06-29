using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ExcelService.Export;

/// <inheritdoc/>
public sealed class ExcelExporter : IExcelExporter
{
    /// <inheritdoc/>
    public MemoryStream ExportData<T>(
        IEnumerable<T> data,
        string sheetName = "Sheet 1",
        CultureInfo? cultureInfo = null
    ) => ExportData(data.ToArray(), sheetName, cultureInfo);

    /// <inheritdoc/>
    public MemoryStream ExportData<T>(
        ICollection<T> data,
        string sheetName = "Sheet 1",
        CultureInfo? cultureInfo = null)
    {
        var stream = new MemoryStream();
        using var document = SpreadsheetDocument.Create(
            stream,
            SpreadsheetDocumentType.Workbook
        );

        var workbookPart = document.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();
        var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        worksheetPart.Worksheet = new Worksheet();

        var sheets = workbookPart.Workbook.AppendChild(new Sheets());
        sheets.Append(new Sheet
        {
            Id = workbookPart.GetIdOfPart(worksheetPart),
            SheetId = 1,
            Name = sheetName
        });
        var sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

        var typeMetadata = GetTypeMetadata(typeof(T));
        ConstructHeader(sheetData, typeMetadata);

        workbookPart.Workbook.Save();
        return stream;
    }

    private static List<ColumnInfo> GetTypeMetadata(Type type)
    {
        var properties = type.GetProperties();
        var result = new List<ColumnInfo>(properties.Length);
        for (ushort i = 0; i < properties.Length; i++)
        {
            result.Add(new ColumnInfo(i, properties[i]));
        }
        return result;
    }

    private static void ConstructHeader(SheetData worksheet, List<ColumnInfo> columnInfo)
    {
        var row = new Row();
        for (ushort i = 0; i < columnInfo.Count; i++)
        {
            row.AppendChild(new Cell
            {
                CellValue = new CellValue(columnInfo[i].Name)
            });
        }
        worksheet.AppendChild(row);
    }
}