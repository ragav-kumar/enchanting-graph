using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

/// <summary>
/// Shunts the specified element into the alt path, and lets everything else through the main path.
/// </summary>
public class FilterNode : NodeBase
{
    public Element FilterElement { get; }
    public float Efficiency { get; }

    public FilterNode(Element filterElement, float efficiency)
    {
        FilterElement = filterElement;
        Efficiency = efficiency;
        ConnectedInputs = new FixedList<bool>(1);
        ConnectedOutputs = new FixedList<bool>(2);
    }

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is FilterNode node)
        {
            return Math.Abs(node.Efficiency - Efficiency) < 0.001f
                && node.FilterElement == FilterElement;;
        }
        return false;
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs)
    {
        Packet inputPacket = inputs.Sum();
        InvalidNodePlacementException.ThrowIfEffectSet(inputPacket.Effect);

        ElementDictionary filtered = inputPacket.Elements * (1 - Efficiency);
        filtered[FilterElement] = inputPacket.Elements[FilterElement] * Efficiency;

        ElementDictionary leftover = inputPacket.Elements * Efficiency;
        leftover[FilterElement] = inputPacket.Elements[FilterElement] * (1 - Efficiency);

        return new Dictionary<int, Packet>
        {
            [0] = inputPacket with { Elements = leftover },
            [1] = inputPacket with { Elements = filtered }
        };
    }
}