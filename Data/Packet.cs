using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

public struct Packet
{
    public required ElementDictionary Elements { get; set; }
    public bool IsBurst { get; set; }
    public PortType? Port { get; set; }
    public EnchantmentEffect? Effect { get; set; }
    public Keyword? Keyword { get; set; }
}