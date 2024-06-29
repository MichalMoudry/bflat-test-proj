using ExcelService.Cli;
using ExcelService.Export;

Console.WriteLine("Hello, World!");

var exporter = new ExcelExporter();
var data = Enumerable
    .Range(0, 15)
    .Select(i => new TestClass());
var stream = exporter.ExportData(data);

await using var fileStream = File.Create("C:/Users/moudr/Downloads/test.xlsx");
stream.Seek(0, SeekOrigin.Begin);
await stream.CopyToAsync(fileStream);

Console.WriteLine("Finished export...");