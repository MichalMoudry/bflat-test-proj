using XmlParser.Core;

namespace XmlParser.UnitTests;

[TestFixture]
public sealed class ReaderServiceTests
{
    [Test]
    public void TestReadFromFilePath()
    {
        var (result, err) = ReaderService.ParseWorkbook<TestData>(
            @""
        );

        Assert.Multiple(() =>
        {
            Assert.That(err, Is.Null);
            Assert.That(result, Is.Not.Empty);
        });
    }

    private sealed class TestData
    {
        public string? Country { get; init; }
        public int Year { get; init; }
        public long Pop { get; init; }
        public string? Continent { get; init; }
        public double LifeExp { get; init; }
        public float GdpPercap { get; init; }
    }
}