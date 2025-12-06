namespace EnchantingGraph.Data;

public class SplitterNode : NodeBase
{
    public SplitterNode(int inputCount, int outputCount)
    {
        if (inputCount is < 1 or > 3)
            throw new ArgumentOutOfRangeException(nameof(inputCount));
        if (outputCount is < 1 or > 3)
            throw new ArgumentOutOfRangeException(nameof(outputCount));
        
        ConnectedInputs = new FixedList<bool>(inputCount);
        ConnectedOutputs = new FixedList<bool>(outputCount);
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs) =>
        EmitPacketsEvenly(inputs.Sum());

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return other is SplitterNode;
    }
}