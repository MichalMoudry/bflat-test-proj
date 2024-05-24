namespace XlsxParser.Core.Model;

public sealed class Row(Cell[] cells)
{
    public Cell[] Cells => cells;
}