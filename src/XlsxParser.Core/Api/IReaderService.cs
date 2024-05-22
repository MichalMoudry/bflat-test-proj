using XlsxParser.Core.Model;

namespace XlsxParser.Core.Api;

/// <summary>
/// A service class with methods for reading contents of Excel files.
/// </summary>
public interface IReaderService
{
    /// <summary>
    /// Method for parsing contents (Stream) of an Excel file into a collection of <typeparamref name="T"/>.
    /// </summary>
    ParsingResult<T> ParseWorkbook<T>(Stream file) where T : new();
}