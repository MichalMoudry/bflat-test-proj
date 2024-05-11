using ClosedXML.Excel;

namespace XmlParser.Core;

public sealed class ReaderService
{
    public void ReadWorksheets(string file)
    {
        using var workbook = new XLWorkbook(file);
        foreach (var worksheet in workbook.Worksheets)
        {
        }
    }
}