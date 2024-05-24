# Test project - bflat
A repository with a sample project that uses
[bflat tooling](https://github.com/bflattened/bflat "Link to bflat project")
for AoT compilation.

One of the projects revolves around parsing Excel files.

## Excel read - performance
### Version 1
| Method                 | Mean     | Error    | StdDev   | Gen0      | Gen1      | Allocated |
|----------------------- |---------:|---------:|---------:|----------:|----------:|----------:|
| TestShortExcelRead     | 38.61 ms | 0.556 ms | 0.493 ms | 2000.0000 |  333.3333 |  16.77 MB |
| TestLongExcelSheetRead | 80.83 ms | 0.803 ms | 0.712 ms | 5000.0000 | 1000.0000 |  45.08 MB |
### Version 2
| Method                       | Mean     | Error    | StdDev   | Gen0      | Gen1      | Allocated |
|----------------------------- |---------:|---------:|---------:|----------:|----------:|----------:|
| TestShortExcelRead           | 38.45 ms | 0.725 ms | 1.396 ms | 2000.0000 |  333.3333 |  16.77 MB |
| TestLongExcelSheetRead       | 82.23 ms | 1.222 ms | 1.020 ms | 5000.0000 | 1000.0000 |  45.08 MB |
| TestShortExcelReadWithFsharp | 13.11 ms | 0.031 ms | 0.028 ms |  968.7500 |   31.2500 |   7.77 MB |
