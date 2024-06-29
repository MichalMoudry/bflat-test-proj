using System.ComponentModel.DataAnnotations;

namespace ExcelService.Cli;

/// <summary>
/// A class used for testing purposes of the Excel service.
/// </summary>
internal sealed class TestClass
{
    public string? Description { get; set; }

    [Display(Order = 0)]
    public long Id { get; set; }

    public int Number { get; set; }

    public short Short { get; set; }

    public double Double { get; set; }

    public float Float { get; set; }

    public decimal Decimal { get; set; }

    public TestEnum Enum { get; set; }

    public DateTimeOffset Added { get; set; }

    public DateTime Updated { get; set; }

    public DateOnly RandomDate { get; set; }

    public Guid ConcurrencyStamp { get; set; }
}