namespace EnchantingGraph.Data;

/// <summary>
/// Removes the selected element from the mana stream.
/// Also removes some portion of the rest of the mana, the Loss factor.
/// </summary>
public class DissipatorNode : NodeBase
{
    public Element Element { get; }
    public float Loss { get; }

    public DissipatorNode(Element element, float loss)
    {
        Element = element;
        Loss = loss;
        ConnectedInputs = new FixedList<bool>(1);
        ConnectedOutputs = new FixedList<bool>(1);
    }

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is DissipatorNode node)
        {
            return node.ConnectedInputs.Equals(ConnectedInputs)
                && node.ConnectedOutputs.Equals(ConnectedOutputs)
                && node.Element == Element
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