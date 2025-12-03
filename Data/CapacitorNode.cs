namespace EnchantingGraph.Data;

public record CapacitorNode : INode
{
    public required Element Element { get; init; }
    public required float PacketSize { get; init; }
    public bool SupportsAltPath => false;
}