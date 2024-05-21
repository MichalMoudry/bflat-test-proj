// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using XmlParser.UnitTests.Performance;

BenchmarkRunner.Run<ExcelReadPerfTests>();