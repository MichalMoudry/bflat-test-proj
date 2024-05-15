using System.Text;

namespace XmlParser.Core.Errors;

/// <summary>
/// A class containing constants for errors related to parsing or other functionality.
/// </summary>
internal static class Errs
{
    public const string InvalidFirstRow = "First row is invalid";

    public const string ZeroProperties = "Class has no properties or column name attributes";

    public static CompositeFormat InvalidCell =
        CompositeFormat.Parse("Unable to process value: {0} ({1} column)");
}