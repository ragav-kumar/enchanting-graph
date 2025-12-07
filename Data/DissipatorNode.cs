namespace EnchantingGraph.Data;

/// <summary>
/// Removes the selected element from the mana stream.
/// Also removes some portion of the rest of the mana, the Loss factor.
/// </summary>
public class DissipatorNode : NodeBase
{
    public Element Element { get; }
    public float Loss { get; }

    public DissipatorNode(Element element, float loss) : base(1,1)
    {
        Element = element;
        Loss = loss;
    }

    public override string ToString() => $"Dissipator: Element={Element}, with {MathF.Round(100 * Loss)}% loss";

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is DissipatorNode node)
        {
            return node.Element == Element
                && Math.Abs(node.Loss - Loss) < 0.001f;
        }
        return false;
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs)
    {
        Packet packet = inputs.Sum();
        InvalidNodePlacementException.ThrowIfEffectSet(packet.Effect);
        
        packet.Elements.Remove(Element);
        packet.Elements *= Loss;
        return EmitPacketsEvenly(packet);
    }
}