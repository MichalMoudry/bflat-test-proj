using XmlParser.Core;
using XmlParser.Core.Attributes;

namespace XmlParser.UnitTests;

[TestFixture]
public sealed class ReaderServiceTests
{
    [Test]
    public void TestReadFromFilePath()
    {
        var result = ReaderService.ParseWorkbook<TestData>(
            string.Empty
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
        Assert.Throws<ArgumentException>(() =>
            ReaderService.ParseWorkbook<TestData>(string.Empty)
        );
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