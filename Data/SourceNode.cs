namespace EnchantingGraph.Data;

/// <summary>
/// The starting point of every enchantment. There can only be one in a given enchantment.
/// Emits packets across all connected inputs. Teh net mana per tick is defined by its total Elements
/// </summary>
public class SourceNode : NodeBase
{
    public ElementDictionary Elements { get; }
    
    public SourceNode(ElementDictionary elements)
    {
        Elements = elements;
        ConnectedInputs = new FixedList<bool>(0);
        ConnectedOutputs = new FixedList<bool>(6);
    }

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is SourceNode node)
        {
            return node.Elements.Equals(Elements);
        }
        return false;
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs) =>
        EmitPacketsEvenly(new Packet { Elements = Elements });
}