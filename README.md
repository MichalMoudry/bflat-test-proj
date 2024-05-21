# Test project - bflat
A repository with a sample project that uses
[bflat tooling](https://github.com/bflattened/bflat "Link to bflat project")
for AoT compilation.

One of the projects revolves around parsing Excel files.

## Excel read - performance
| Method                 | Mean      | Error    | StdDev   | Gen0       | Gen1      | Allocated |
|----------------------- |----------:|---------:|---------:|-----------:|----------:|----------:|
| TestShortExcelRead     |  68.24 ms | 1.078 ms | 0.955 ms |  5666.6667 |  666.6667 |  16.78 MB |
| TestLongExcelSheetRead | 148.41 ms | 2.777 ms | 3.983 ms | 17000.0000 | 1000.0000 |  45.12 MB |
