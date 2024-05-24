# Test project - bflat
A repository with a sample project that uses
[bflat tooling](https://github.com/bflattened/bflat "Link to bflat project")
for AoT compilation.

One of the projects revolves around parsing Excel files.

## Excel read - performance
| Method                       | Mean      | Error    | StdDev   | Gen0        | Gen1      | Gen2      | Allocated  |
|----------------------------- |----------:|---------:|---------:|------------:|----------:|----------:|-----------:|
| TestShortExcelRead           |  39.52 ms | 0.299 ms | 0.249 ms |   2000.0000 |  333.3333 |         - |   16.76 MB |
| TestLongExcelSheetRead       |  83.19 ms | 0.904 ms | 0.845 ms |   5000.0000 | 1000.0000 |         - |   44.73 MB |
| TestLargeExcelSheetRead      | 227.10 ms | 4.539 ms | 4.661 ms |  19000.0000 | 4000.0000 | 1000.0000 |  151.98 MB |
| TestShortExcelReadWithFsharp |  13.43 ms | 0.051 ms | 0.045 ms |    953.1250 |   46.8750 |   15.6250 |    7.71 MB |
| TestLongExcelReadWithFsharp  |  57.90 ms | 0.343 ms | 0.286 ms |  12500.0000 |  250.0000 |         - |   101.6 MB |
| TestLargeExcelReadWithFsharp | 852.38 ms | 2.094 ms | 1.856 ms | 316000.0000 | 1000.0000 |         - | 2527.55 MB |
