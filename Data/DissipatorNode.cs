namespace EnchantingGraph.Data;

/// <summary>
/// Removes the selected element from the mana stream.
/// Also removes some portion of the rest of the mana, the Loss factor.
/// </summary>
public record DissipatorNode : INode
{
    public required Element Element { get; init; }
    public required float Loss { get; init; }
    public bool SupportsAltPath => false;
}