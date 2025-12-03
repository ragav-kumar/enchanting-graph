namespace EnchantingGraph.Data;

public record FilterNode : INode
{
    public required Element FilterElement { get; init; }
    public bool SupportsAltPath => true;
}