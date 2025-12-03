namespace EnchantingGraph.Data;

public record DissipatorNode : INode
{
    public required Element Element { get; init; }
    public required float Loss { get; init; }
    public bool SupportsAltPath => false;
}