// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using XlsxParser.UnitTests.Performance;

BenchmarkRunner.Run<ExcelReadPerfTests>();