namespace EnchantingGraph.Data;

/// <summary>
/// Accumulates smaller packets into bigger ones, but with a lower rate.
/// Effectively gives the enchantment a cooldown time, in exchange for higher peaks.
/// </summary>
public class CapacitorNode : NodeBase
{
    public float PacketSize { get; }
    private ElementDictionary currentStorage_ = new();

    public CapacitorNode(float packetSize) : base(1,1)
    {
        PacketSize = packetSize;
    }

    public override bool Equals(NodeBase? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other is CapacitorNode node)
        {
            return Math.Abs(node.PacketSize - PacketSize) < 0.001f
                && node.currentStorage_ == currentStorage_;
        }
        return false;
    }

    public override Dictionary<int, Packet>? Simulate(Dictionary<int, Packet> inputs)
    {
        Packet totalInput = inputs.Sum();
        InvalidNodePlacementException.ThrowIfEffectSet(totalInput.Effect);
        
        currentStorage_ += totalInput.Elements;
        if (currentStorage_.Magnitude >= PacketSize)
        {
            ElementDictionary packet = currentStorage_.Packet(PacketSize);
            currentStorage_ -= packet;
            return EmitPacketsEvenly(new Packet
            {
                IsBurst = true,
                Elements = packet,
            });
        }
        return null;
    }

    public override string ToString() => $"Capacitor: PacketSize={PacketSize}, Currently storing {currentStorage_}";
}