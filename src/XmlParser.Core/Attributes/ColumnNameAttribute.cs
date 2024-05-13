namespace XmlParser.Core.Attributes;

/// <summary>
/// An attribute for assigning class properties to Excel column.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ColumnNameAttribute(string name) : Attribute
{
    /// <summary>
    /// Name of the column in the Excel file.
    /// </summary>
    public string Name => name;
}