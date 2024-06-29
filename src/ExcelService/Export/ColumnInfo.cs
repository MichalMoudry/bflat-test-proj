using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ExcelService.Export;

internal sealed class ColumnInfo(int index, PropertyInfo property)
{
    public int Index => index;

    public string Name => property.Name;

    public int? Order =>
        property.GetCustomAttribute<DisplayAttribute>()?.Order;

    public bool IsEnum => property.PropertyType.IsEnum;

    public PropertyInfo PropertyInfo => property;
}