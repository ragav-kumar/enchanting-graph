using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

/// <summary>
/// Converts InElement to OutElement at the given efficiency.
/// No mana is lost, efficiency only determines how much of the input mana is converted.
/// </summary>
public class ConverterNode : NodeBase
{
    public Element InElement { get; }
    public Element OutElement { get; }
    public float Efficiency { get; }

    public ConverterNode(Element inElement, Element outElement, float efficiency) : base(3,3)
    {
        InElement = inElement;
        OutElement = outElement;
        Efficiency = efficiency;
    }

    public override string ToString() => $"Converter: {InElement} to {OutElement} @ {MathF.Round(100 * Efficiency)}% efficiency";

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is ConverterNode node)
        {
            return node.InElement == InElement
                && node.OutElement == OutElement
                && Math.Abs(node.Efficiency - Efficiency) < 0.001f;
        }
        return false;
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs)
    {
        Packet packet = inputs.Sum();
        InvalidNodePlacementException.ThrowIfEffectSet(packet.Effect);
        
        float inElement = packet.Elements[InElement];
        if (inElement > 0f)
        {
            packet.Elements[InElement] = inElement * (1 - Efficiency);
            packet.Elements[OutElement] += inElement * Efficiency;
        }
        return EmitPacketsEvenly(packet);
    }
}