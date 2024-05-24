using FSharp.Interop.Excel;
using XlsxParser.UnitTests.Model;
using XlsxParser.UnitTests.TestData;
using XlsxParser.Core;

namespace XlsxParser.UnitTests;

[TestFixture]
public sealed class ReaderServiceTests
{
    private readonly ReaderService _readerService = new();

    [Test]
    public void TestReadFromMemoryStream()
    {
        var result = _readerService
            .ParseWorkbook<TestCountryData>(new MemoryStream(Resources.SimpleShortExcel))
            .Select(i => new TestCountryData
            {
                Country = i.Cells[0].Value,
                Year = Convert.ToInt32(i.Cells[1].Value),
                Population = Convert.ToInt64(i.Cells[2].Value),
                Continent = i.Cells[3].Value,
                LifeExpectancy = i.Cells[4].Value,
                GdpPerCapita = i.Cells[5].Value
            })
            .ToArray();
        TestCountryData[] expected = [
            new TestCountryData
            {
                Country = "Test country",
                Year = 1952,
                Population = 118_425_333,
                Continent = "Test cont",
                GdpPerCapita = "479.4453145",
                LifeExpectancy = "28.801"
            },
            new TestCountryData
            {
                Country = "Test country",
                Year = 1957,
                Population = 119240934,
                Continent = "Test cont",
                GdpPerCapita = "520.8530296",
                LifeExpectancy = "30.332"
            }
        ];
        Assert.That(result, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestReadFromEmptyStream()
    {
        var res = _readerService.ParseWorkbook<TestCountryData>(
            new MemoryStream()
        );

        Assert.That(res, Is.Empty);
    }

    [Test]
    public void TestFSharpReaderService()
    {
        var readerService = new XlsxParser.Core.FSharp.ReaderService();
        var res = readerService.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleShortExcel),
            ExcelFormat.Xlsx,
            "List1",
            string.Empty
        ).ToArray();
        Console.WriteLine(res);
    }
}