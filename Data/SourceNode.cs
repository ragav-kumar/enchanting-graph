using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

/// <summary>
/// The starting point of every enchantment. There can only be one in a given enchantment.
/// Emits mana in packets of the provided size.
/// </summary>
public record SourceNode : INode
{
    public required Element Element { get; init; }
    public required float PacketSize { get; init; }
    public bool SupportsAltPath => false;

    public bool TryAppend(Enchantment enchantment)
    {
        enchantment.Element = Element;
        enchantment.Magnitude = PacketSize;
        return true;
    }
}