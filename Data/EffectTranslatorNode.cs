using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

public class EffectTranslatorNode : NodeBase
{
    public EnchantmentEffect Effect { get; }
    public Element Element { get; }
    public float Efficiency { get; }

    public EffectTranslatorNode(EnchantmentEffect effect, Element element, float efficiency) : base(4,1)
    {
        Effect = effect;
        Element = element;
        Efficiency = efficiency;
    }

    public override string ToString() => $"Effect Translator: Effect={Effect}, Element={Element}, at {MathF.Round(100 * Efficiency)}% efficiency";

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is EffectTranslatorNode node)
        {
            return node.Effect == Effect
                && node.Element == Element
                && Math.Abs(node.Efficiency - Efficiency) < 0.001f;
        }
        return false;
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs)
    {
        Packet packet = inputs.Sum();
        packet.Effect = Effect;
        return new Dictionary<int, Packet>
        {
            [0] = packet with
            {
                Effect = Effect,
                Elements = new ElementDictionary
                {
                    [Element] = packet.Elements[Element] * Efficiency
                }
            }
        };
    }
}