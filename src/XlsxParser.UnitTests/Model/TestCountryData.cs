using XlsxParser.Core.Attributes;

namespace XlsxParser.UnitTests.Model;

internal sealed class TestCountryData : IEquatable<TestCountryData>
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
    public string? LifeExpectancy { get; init; }
    [ColumnName("GdpPercap")]
    public string? GdpPerCapita { get; init; }

    public bool Equals(TestCountryData? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return
            Country == other.Country
            && Year == other.Year
            && Population == other.Population
            && Continent == other.Continent
            && LifeExpectancy == other.LifeExpectancy
            && GdpPerCapita == other.GdpPerCapita;
    }

    public override bool Equals(object? obj) =>
        ReferenceEquals(this, obj)
        || obj is TestCountryData other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(
        Country,
        Year,
        Population,
        Continent,
        LifeExpectancy,
        GdpPerCapita
    );
}