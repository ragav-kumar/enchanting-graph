namespace EnchantingGraph.Data;

public abstract class NodeBase(int inputCount, int outputCount)
{
    public FixedList<bool> ConnectedInputs { get; } = new(inputCount);
    public FixedList<bool> ConnectedOutputs { get; } = new(outputCount);

    protected Dictionary<int, Packet>? EmitPacketsEvenly(Packet packet)
    {
        int numPackets = ConnectedOutputs.Count(o => o);
        if (numPackets == 0)
        {
            return null;
        }
        
        Packet emitted = packet with
        {
            Elements = packet.Elements / numPackets
        };
        
        Dictionary<int, Packet> dict = new(numPackets);
        for (int i = 0; i < ConnectedOutputs.Count; i++)
        {
            if (ConnectedOutputs[i])
            {
                dict[i] = emitted;
            }
        }

        return dict;
    }

    public abstract Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs);
    public abstract bool Equals(NodeBase? other);
}