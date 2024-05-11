namespace XmlParser.Core.Model;

/// <summary>
/// An internal subsection of PropertyInfo class.
/// </summary>
internal readonly record struct PropertyData(
    string PropertyName,
    Type PropertyType
);