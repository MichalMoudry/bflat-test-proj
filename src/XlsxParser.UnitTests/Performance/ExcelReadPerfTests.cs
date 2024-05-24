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
    private XlsxParser.Core.FSharp.ReaderService? _fsReaderService;

    [GlobalSetup]
    public void Setup()
    {
        _readerService = new ReaderService();
        _fsReaderService = new Core.FSharp.ReaderService();
    }

    [Benchmark]
    public void TestShortExcelRead()
    {
        var res = _readerService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleShortExcel)
        );
        var amount = res.Count();
        Console.WriteLine(amount);
    }

    [Benchmark]
    public void TestLongExcelSheetRead()
    {
        var res = _readerService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleLongExcel)
        );
        var amount = res.Count();
        Console.WriteLine(amount);
    }

    [Benchmark]
    public void TestLargeExcelSheetRead()
    {
        var res = _readerService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleLargeExcel)
        );
        var amount = res.Count();
        Console.WriteLine(amount);
    }

    [Benchmark]
    public void TestShortExcelReadWithFsharp()
    {
        var res = _fsReaderService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleShortExcel),
            ExcelFormat.Xlsx,
            "List1",
            string.Empty
        );
        var amount = res.Count();
        Console.WriteLine(amount);
    }

    [Benchmark]
    public void TestLongExcelReadWithFsharp()
    {
        var res = _fsReaderService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleLongExcel),
            ExcelFormat.Xlsx,
            "List1",
            string.Empty
        );
        var amount = res.Count();
        Console.WriteLine(amount);
    }

    [Benchmark]
    public void TestLargeExcelReadWithFsharp()
    {
        var res = _fsReaderService!.ParseWorkbook<TestCountryData>(
            new MemoryStream(Resources.SimpleLargeExcel),
            ExcelFormat.Xlsx,
            "List1",
            string.Empty
        );
        var amount = res.Count();
        Console.WriteLine(amount);
    }
}