using XmlParser.Core;
using XmlParser.Core.Attributes;
using XmlParser.UnitTests.TestData;

namespace XmlParser.UnitTests;

[TestFixture]
public sealed class ReaderServiceTests
{
    [Test]
    [Ignore("For local development")]
    public void TestReadFromFilePath()
    {
        var result = ReaderService.ParseWorkbook<TestData>(
            new MemoryStream(0)
        );

        Assert.Multiple(() =>
        {
            Assert.That(result.Error, Is.Null);
            Assert.That(result.Data, Is.Not.Empty);
        });
    }

    [Test]
    public void TestReadFromByteArray()
    {
        var result = ReaderService.ParseWorkbook<TestData>(
            new MemoryStream(Resources.SimpleShortExcel)
        );

        Assert.Multiple(() =>
        {
            Assert.That(result.Error, Is.Null);
            Assert.That(result.Data, Is.Not.Empty);
        });
    }

    [Test]
    public void TestReadFromEmptyFile()
    {
        //Assert.Throws<ArgumentException>(() => ReaderService.ParseWorkbook<TestData>(string.Empty));
    }

    private sealed class TestData
    {
        [ColumnName("Country")]
        public string? Country { get; init; }
        [ColumnName("Year")]
        public int Year { get; init; }
        [ColumnName("Pop")]
        public long Population { get; init; }
        [ColumnName("Continent")]
        public string? Continent { get; init; }
        [ColumnName("LifeExp")]
        public double LifeExpectancy { get; init; }
        [ColumnName("GdpPercap")]
        public float GdpPerCapita { get; init; }
    }
}