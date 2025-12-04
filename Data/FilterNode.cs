using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

/// <summary>
/// Shunts the specified element into the alt path, and lets everything else through the main path.
/// </summary>
public record FilterNode : INode
{
    public required Element FilterElement { get; init; }
    public bool SupportsAltPath => true;
    public bool TryAppend(Enchantment enchantment)
    {
        throw new NotImplementedException();
    }
}