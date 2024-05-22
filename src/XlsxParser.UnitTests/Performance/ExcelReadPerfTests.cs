using BenchmarkDotNet.Attributes;
using XlsxParser.UnitTests.Model;
using XlsxParser.UnitTests.TestData;
using XlsxParser.Core;

namespace XlsxParser.UnitTests.Performance;

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