namespace EnchantingGraph.Data;

public record PortNode : INode
{
    public PortType Type { get; init; }
    public bool SupportsAltPath => false;
}