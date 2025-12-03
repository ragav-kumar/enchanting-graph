namespace EnchantingGraph.Data;

public record EffectTranslatorNode : INode
{
    public required EnchantmentEffect Effect { get; init; }
    public required Element Element { get; init; }
    public required float Efficiency { get; init; }
    public bool SupportsAltPath => false;
}