namespace EnchantingGraph.Data;

/// <summary>
/// Activated manually on keyword.
/// </summary>
public record TriggerNode : INode
{
    public required Keyword Keyword { get; init; }
    public required float MaxPacketSize { get; init; }
    public bool SupportsAltPath => false;
}