namespace EnchantingGraph.Data;

public record SourceNode : INode
{
    public required Element Element { get; init; }
    public required float PacketSize { get; init; }
    public bool SupportsAltPath => false;
}