using EnchantingGraph.Data;

namespace EnchantingGraph.Effects;

public class Enchantment
{
    public EnchantmentEffect Effect { get; set; }
    public EffectMode Mode { get; set; }
    public PortType Port { get; set; }
    public ElementDictionary Elements { get; set; } = new();
    public float CooldownTime { get; set; } = 1.0f;
    public float ChargeUpTime { get; set; } = 0.0f;
    public float Range { get; set; } = 1.0f;
    public float Duration { get; set; } = 0.0f;

    public float Magnitude => Elements.Values.Sum();

    public override string ToString()
    {
        string elementMagnitudes = string.Join("; ", Elements
            .Select(pair => $"{pair.Key}: {pair.Value}")
        );
        string description = $"""
                              Effect: {Effect}
                              Mode: {Mode}
                              Port: {Port}
                              Elements: {elementMagnitudes}
                              Range: {Range}
                              Duration: {Duration}
                              """;
        if (CooldownTime > 0f)
        {
            description += $"\nCooldown: {CooldownTime}s";
        }

        if (ChargeUpTime > 0f)
        {
            description += $"\nCharge up time: {ChargeUpTime}s";
        }

        return description;
    }
}