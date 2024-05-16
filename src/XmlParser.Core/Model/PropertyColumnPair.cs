namespace XmlParser.Core.Model;

/// <summary>
/// A record encapsulating a connection between a property name and a Excel column name.
/// </summary>
internal sealed record PropertyColumnPair(
    string PropertyName,
    string? ColumnName
);