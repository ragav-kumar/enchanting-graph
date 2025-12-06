namespace EnchantingGraph.Data;

/// <summary>
/// Activated manually on keyword.
/// </summary>
public class TriggerNode : NodeBase
{
    public Keyword Keyword { get; }

    public TriggerNode(Keyword keyword)
    {
        Keyword = keyword;
        ConnectedInputs = new FixedList<bool>(1);
        ConnectedOutputs = new FixedList<bool>(1);
    }

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is TriggerNode node)
        {
            return node.Keyword.Equals(Keyword);
        }
        return false;
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs)
    {
        Packet packet = inputs.Sum();
        return EmitPacketsEvenly(packet with { Keyword = Keyword });
    }
}