using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

/// <summary>
/// Accumulates smaller packets into bigger ones, but with a lower rate.
/// Effectively gives the enchantment a cooldown time, in exchange for higher peaks.
/// </summary>
public record CapacitorNode : INode
{
    public required Element Element { get; init; }
    public required float PacketSize { get; init; }
    public bool SupportsAltPath => false;

    public bool TryAppend(Enchantment enchantment)
    {
        if (enchantment.Element != Element)
        {
            return false;
        }
        
        float multiplier = PacketSize / enchantment.Magnitude;
        enchantment.Magnitude = PacketSize;
        enchantment.CooldownTime += multiplier;
        return true;
    }
}