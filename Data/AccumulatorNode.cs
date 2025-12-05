using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

/// <summary>
/// Special kind of capacitor. Unlike a normal capacitor, it's output packet size
/// is dissociated from its storage capacity. Once triggered, it will
/// keep discharging until empty.
/// Any incoming mana will continue to accumulate during discharge.
/// </summary>
public class AccumulatorNode : NodeBase
{
    public float PacketSize { get; }
    public float Capacity { get; }
    public bool IsDischarging { get; private set; }
    private ElementDictionary currentStorage_ = new();

    public AccumulatorNode(float packetSize, float capacity)
    {
        PacketSize = packetSize;
        Capacity = capacity;
        ConnectedInputs = new FixedList<bool>(3);
        ConnectedOutputs = new FixedList<bool>(1);
    }

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is AccumulatorNode node)
        {
            return node.currentStorage_ == currentStorage_
                && node.IsDischarging == IsDischarging
                && Math.Abs(node.Capacity - Capacity) < 0.001f
                && node.ConnectedInputs.Equals(ConnectedInputs)
                && node.ConnectedOutputs.Equals(ConnectedOutputs)
                && Math.Abs(node.PacketSize - PacketSize) < 0.001f;
        }

        return false;
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs)
    {
        Packet inputPacket = inputs.Sum();
        InvalidNodePlacementException.ThrowIfEffectSet(inputPacket.Effect);
        
        currentStorage_ += inputPacket.Elements;
        float currentMagnitude = currentStorage_.Magnitude;

        if (currentMagnitude > Capacity)
        {
            IsDischarging = true;
        }
        else if (currentMagnitude < PacketSize)
        {
            IsDischarging = false;
        }
        
        if (!IsDischarging)
        {
            return null;
        }
        
        ElementDictionary packet = currentStorage_.Packet(PacketSize);
        currentStorage_ -= packet;
        return EmitPacketsEvenly(new Packet
        {
            IsBurst = true,
            Elements = packet,
        });
    }
}