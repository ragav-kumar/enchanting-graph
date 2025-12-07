namespace EnchantingGraph.Data;

public class SplitterNode : NodeBase
{
    public SplitterNode(int inputCount, int outputCount) : base(inputCount, outputCount)
    {
        if (inputCount is < 1 or > 3)
            throw new ArgumentOutOfRangeException(nameof(inputCount));
        if (outputCount is < 1 or > 3)
            throw new ArgumentOutOfRangeException(nameof(outputCount));
    }

    public override string ToString()
    {
        int inCount = ConnectedInputs.Count(o => o);
        int outCount = ConnectedOutputs.Count(o => o);
        return $"Splitter: {inCount} in, {outCount} out";
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