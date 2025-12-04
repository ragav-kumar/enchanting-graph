using EnchantingGraph.Data;
using EnchantingGraph.Effects;

namespace EnchantingGraph;

public static class InitializerData
{
    private static SourceNode WaterSource => new()
    {
        Element = Element.Water,
        PacketSize = 10f,
    };

    private static CapacitorNode WaterCapacitor => new()
    {
        Element = Element.Water,
        PacketSize = 50f,
    };

    private static ConverterNode ConvertWaterToMetal => new()
    {
        InElement = Element.Water,
        OutElement = Element.Metal,
        Efficiency = 0.7f,
    };

    private static ConverterNode ConvertWaterToAir => new()
    {
        InElement = Element.Water,
        OutElement = Element.Air,
        Efficiency = 0.9f,
    };

    private static ConverterNode ConvertAirToFire => new()
    {
        InElement = Element.Air,
        OutElement = Element.Fire,
        Efficiency = 0.88f,
    };

    private static EffectTranslatorNode InfuseFire => new()
    {
        Effect = EnchantmentEffect.Infusion,
        Element = Element.Fire,
        Efficiency = 1.0f
    };

    private static EffectTranslatorNode EmitMetal => new()
    {
        Effect = EnchantmentEffect.Emission,
        Element = Element.Metal,
        Efficiency = 1.2f,
    };

    private static PortNode StructurePort => new()
    {
        Type = PortType.Structure
    };

    private static PortNode TargetPort => new()
    {
        Type = PortType.Target
    };

    public static readonly List<NodePathElement> SinglePath =
    [
        new() { Node = WaterSource, NextNodes = [WaterCapacitor] },
        new() { Node = WaterCapacitor, NextNodes = [ConvertWaterToMetal] },
        new() { Node = ConvertWaterToMetal, NextNodes = [EmitMetal] },
        new() { Node = EmitMetal, NextNodes = [StructurePort] },
        new() { Node = StructurePort }
    ];

    public static readonly List<NodePathElement> SingleSplit =
    [
        new() { Node = WaterSource, NextNodes = [WaterCapacitor, ConvertWaterToMetal] },
        new() { Node = ConvertWaterToMetal, NextNodes = [EmitMetal] },
        new() { Node = EmitMetal, NextNodes = [StructurePort] },
        new() { Node = StructurePort },
        new() { Node = WaterCapacitor, NextNodes = [ConvertWaterToAir] },
        new() { Node = ConvertWaterToAir, NextNodes = [ConvertAirToFire] },
        new() { Node = ConvertAirToFire, NextNodes = [InfuseFire] },
        new() { Node = InfuseFire, NextNodes = [TargetPort] },
        new() { Node = TargetPort }
    ];
}