using EnchantingGraph.Data;

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

    private static EffectTranslatorNode ProjectMetal => new()
    {
        Effect = EnchantmentEffect.Project,
        Element = Element.Metal,
        Efficiency = 1.2f,
    };

    private static PortNode StructurePort => new()
    {
        Type = PortType.Structure
    };

    public static readonly List<NodePathElement> SinglePath =
    [
        new() { Node = WaterSource, NextNodes = [WaterCapacitor] },
        new() { Node = WaterCapacitor, NextNodes = [ConvertWaterToMetal] },
        new() { Node = ConvertWaterToMetal, NextNodes = [ProjectMetal] },
        new() { Node = ProjectMetal, NextNodes = [StructurePort] },
        new() { Node = StructurePort }
    ];
}