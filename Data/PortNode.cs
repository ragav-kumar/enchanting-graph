using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

/// <summary>
/// The end point of an enchantment. The Type is a primary determinant of the enchantment.
/// </summary>
public class PortNode : NodeBase
{
    public PortType Type { get; init; }

    public PortNode(PortType type)
    {
        Type = type;
        ConnectedInputs = new FixedList<bool>(4);
        ConnectedOutputs = FixedList<bool>.From([true]);
    }

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is PortNode node)
        {
            return node.Type == Type;
        }
        return false;
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs)
    {
        Packet packet = inputs.Sum();
        if (packet.Elements.Magnitude < 0.001f)
        {
            return null;
        }
        packet.Port = Type;
        return new Dictionary<int, Packet>
        {
            [0] = packet
        };
    }
}