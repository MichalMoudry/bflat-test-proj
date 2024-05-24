using BenchmarkDotNet.Attributes;
using FSharp.Interop.Excel;
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
        var res = _readerService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleShortExcel)
        );
        foreach (var row in res.Data)
        {
            Console.WriteLine(row);
        }
    }

    [Benchmark]
    public void TestLongExcelSheetRead()
    {
        var res = _readerService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleLongExcel)
        );
        foreach (var row in res.Data)
        {
            Console.WriteLine(row);
        }
    }

    [Benchmark]
    public void TestShortExcelReadWithFsharp()
    {
        var readerService = new XlsxParser.Core.FSharp.ReaderService();
        var res = readerService.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleShortExcel),
            ExcelFormat.Xlsx,
            "List1",
            "A1:F3"
        );
        foreach (var row in res)
        {
            Console.WriteLine(row);
        }
    }
}