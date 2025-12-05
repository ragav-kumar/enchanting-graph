using System.Diagnostics.CodeAnalysis;
using EnchantingGraph.Effects;

namespace EnchantingGraph;

public class InvalidNodePlacementException(string message) : InvalidOperationException(message)
{
    public static void ThrowIfEffectSet(EnchantmentEffect? effect)
    {
        if (effect != null)
        {
            throw new InvalidNodePlacementException("Cannot place after effect has been registered.");
        }
    }
}