namespace EnchantingGraph.Data;

public record TriggerNode : INode
{
    public required Keyword Keyword { get; init; }
    public required float MaxPacketSize { get; init; }
    public bool SupportsAltPath => false;
}