using BenchmarkDotNet.Attributes;
using XmlParser.Core;
using XmlParser.UnitTests.Model;
using XmlParser.UnitTests.TestData;

namespace XmlParser.UnitTests.Performance;

/// <summary>
/// Tests covering performance of a <see cref="ReaderService"/> class.
/// </summary>
[MemoryDiagnoser]
public class ExcelReadPerfTests
{
    private ReaderService? _readerService;

    [GlobalSetup]
    public void Setup() => _readerService = new ReaderService();

    [Benchmark]
    public void TestShortExcelRead()
    {
        _readerService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleShortExcel)
        );
    }

    [Benchmark]
    public void TestLongExcelSheetRead()
    {
        _readerService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleLongExcel)
        );
    }
}