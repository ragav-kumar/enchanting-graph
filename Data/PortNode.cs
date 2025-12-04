using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

/// <summary>
/// The end point of an enchantment. The Type is a primary determinant of the enchantment.
/// </summary>
public record PortNode : INode
{
    public PortType Type { get; init; }
    public bool SupportsAltPath => false;
    
    public bool TryAppend(Enchantment enchantment)
    {
        if (enchantment.Mode == EffectMode.Passive && Type == PortType.Target)
        {
            return false;
        }
        
        enchantment.Port = Type;
        return true;
    }
}