using System.Text;
using EnchantingGraph.Effects;

namespace EnchantingGraph.Data;

public struct Packet
{
    public required ElementDictionary Elements { get; set; }
    public bool IsBurst { get; set; }
    public PortType? Port { get; set; }
    public EnchantmentEffect? Effect { get; set; }
    public Keyword? Keyword { get; set; }

    private string EffectString() => Effect switch
    {
        EnchantmentEffect.Emission => "emitted",
        EnchantmentEffect.Infusion => "infused",
        EnchantmentEffect.Absorption => "absorbed",
        EnchantmentEffect.Creation => "created",
        EnchantmentEffect.Destruction => "destroyed",
        _ => throw new ArgumentOutOfRangeException()
    };

    public override string ToString()
    {
        string mode = IsBurst ? "Burst" : "Passively";
        string message = $"{mode} {EffectString()} {Elements} into the {Port} port";
        
        if (Keyword is not null)
        {
            message += $", on keyword \"{Keyword}\"";
        }

        return message;
    }
}