using System.Globalization;
using EnchantingGraph.Data;

namespace EnchantingGraph.Effects;

public class Enchantment
{
    public EnchantmentEffect Effect { get; set; }
    public EffectMode Mode { get; set; }
    public PortType Port { get; set; }
    public Element Element { get; set; } = Element.Neutral;
    public float Magnitude { get; set; }
    public float CooldownTime { get; set; } = 1.0f;
    public float ChargeUpTime { get; set; }
    public float Range { get; set; } = 1.0f;

    private string SwitchOnMode(string instantaneous, string passive) =>
        Mode switch
        {
            EffectMode.Instantaneous => instantaneous,
            EffectMode.Passive => passive,
            _ => throw new ArgumentOutOfRangeException(nameof(Mode))
        };

    /// <summary>
    /// For now, simple descriptions.
    /// </summary>
    private string Description()
    {
        switch (Effect)
        {
            case EnchantmentEffect.Emission:
                return Port switch
                {
                    PortType.Structure => SwitchOnMode(
                        instantaneous: "Triggers a powerful explosion, dealing %M %E damage to all foes within %Rm of you.",
                        passive: "Lengthen the blade of this weapon by %Rm, and grants a damage bonus of %M %E damage."
                    ),
                    PortType.Target => "Unleash a ranged bolt of power, dealing %M %E damage to the foe it strikes.",
                    PortType.Wielder => SwitchOnMode(
                        instantaneous: "",
                        passive: ""
                    ),
                    _ => throw new ArgumentOutOfRangeException()
                };
            case EnchantmentEffect.Infusion:
                return Port switch
                {
                    PortType.Structure => SwitchOnMode(
                        instantaneous: "",
                        passive: ""
                    ),
                    PortType.Target => "",
                    PortType.Wielder => SwitchOnMode(
                        instantaneous: "",
                        passive: ""
                    ),
                    _ => throw new ArgumentOutOfRangeException()
                };
            case EnchantmentEffect.Absorption:
            case EnchantmentEffect.Creation:
            case EnchantmentEffect.Destruction:
                throw new NotImplementedException();
        }

        return "Unknown effect";
    }

    public override string ToString()
    {
        string description = Description()
            .Replace("%M", Magnitude.ToString(CultureInfo.InvariantCulture))
            .Replace("%E", Element.ToString())
            .Replace("%R", Range.ToString(CultureInfo.InvariantCulture));

        if (string.IsNullOrEmpty(description))
        {
            description = "[Placeholder description]";
        }

        if (CooldownTime > 0f)
        {
            description = $"{description}\nEffect has a cooldown of {CooldownTime}s";
        }

        if (ChargeUpTime > 0f)
        {
            description = $"{description}\nEffect has a charge up time of {ChargeUpTime}s";
        }

        return description;
    }
}