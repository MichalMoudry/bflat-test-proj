using XmlParser.Core;
using XmlParser.UnitTests.Model;
using XmlParser.UnitTests.TestData;

namespace XmlParser.UnitTests;

[TestFixture]
public sealed class ReaderServiceTests
{
    private readonly ReaderService _readerService = new();

    [Test]
    [Ignore("For local development")]
    public void TestReadFromFilePath()
    {
        var result = _readerService.ParseWorkbook<TestCountryData>(
            string.Empty
        );

        Assert.Multiple(() =>
        {
            Assert.That(result.Error, Is.Null);
            Assert.That(result.Data, Is.Not.Empty);
        });
    }

    [Test]
    public void TestReadFromMemoryStream()
    {
        var result = _readerService.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleShortExcel)
        );
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

        Assert.Multiple(() =>
        {
            Assert.That(result.Error, Is.Null);
            Assert.That(result.Data, Is.EqualTo(expected));
        });
    }

    [Test]
    public void TestReadFromEmptyStream()
    {
        var res = _readerService.ParseWorkbook<TestCountryData>(
            new MemoryStream()
        );

        Assert.Multiple(() =>
        {
            Assert.That(res.Data, Is.Empty);
            Assert.That(res.Error, Is.EqualTo("File or stream is empty"));
        });
    }
}