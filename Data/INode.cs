using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

public interface INode
{
    /// <summary>
    /// If true, then the node will define behaviour for two paths, instead of just one.
    /// </summary>
    public bool SupportsAltPath { get; }

    public bool TryAppend(Enchantment enchantment);
}