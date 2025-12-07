using EnchantingGraph.Data;
using EnchantingGraph.Effects;

namespace EnchantingGraph.Graph;

public static class InitializerData
{
    private static SourceNode WaterSource => new(new ElementDictionary
    {
        [Element.Water]   = 7.0f,
        [Element.Neutral] = 2.6f,
        [Element.Earth]   = 0.3f,
        [Element.Gray]    = 0.1f,
    });

    private static CapacitorNode Capacitor => new(50f);

    private static ConverterNode ConvertWaterToMetal => new
    (
        inElement: Element.Water,
        outElement: Element.Metal,
        efficiency: 0.7f
    );

    private static ConverterNode ConvertWaterToAir => new
    (
        inElement: Element.Water,
        outElement: Element.Air,
        efficiency: 0.9f
    );

    private static ConverterNode ConvertAirToFire => new
    (
        inElement: Element.Air,
        outElement: Element.Fire,
        efficiency: 0.88f
    );

    private static EffectTranslatorNode InfuseFire => new
    (
        effect: EnchantmentEffect.Infusion,
        element: Element.Fire,
        efficiency: 1.0f
    );

    private static EffectTranslatorNode EmitMetal => new
    (
        effect: EnchantmentEffect.Emission,
        element: Element.Metal,
        efficiency: 1.2f
    );

    private static PortNode StructurePort => new(PortType.Structure);

    private static PortNode TargetPort => new(PortType.Target);

    public static IReadOnlySet<NodePathElement> SinglePath { get; }
    public static IReadOnlySet<NodePathElement> SingleSplit { get; }

    static InitializerData()
    {
        SinglePath =
            new GraphBuilder()
                .Add(WaterSource)
                .WithChild(ConvertWaterToMetal, parentIndex: 0, childIndex: 0)
                .WithChild(EmitMetal, parentIndex: 0, childIndex: 0)
                .WithChild(StructurePort, parentIndex: 0, childIndex: 0)
            .Build()
            .AsReadOnly();

        GraphBuilder singleSplitBuilder = new();
        /*singleSplitBuilder
            .Add(WaterSource)
            .WithChild(ConvertWaterToAir, parentIndex: 0, childIndex: 0)
            .WithChild(ConvertAirToFire, parentIndex: 0, childIndex: 0)
            .WithChild(Capacitor, parentIndex: 0, childIndex: 0)
            .WithChild(InfuseFire, parentIndex: 0 , childIndex: 0)
            .WithChild(TargetPort, parentIndex: 0, childIndex: 0);

        singleSplitBuilder
            .Add(ConvertWaterToMetal, parent: WaterSource, parentIndex: 1, childIndex: 0)
            .WithChild(EmitMetal, parentIndex: 0, childIndex: 0)
            .WithChild(StructurePort, parentIndex: 0, childIndex: 0);*/
        
        singleSplitBuilder
            .Add(WaterSource)
            .WithChild(ConvertWaterToMetal, parentIndex: 0, childIndex: 0)
            .WithChild(EmitMetal, parentIndex: 0, childIndex: 0)
            .WithChild(StructurePort, parentIndex: 0, childIndex: 0);
        
        singleSplitBuilder
            .Add(ConvertWaterToAir, parent: WaterSource, parentIndex: 1, childIndex: 0)
            .WithChild(ConvertAirToFire, parentIndex: 0, childIndex: 0)
            .WithChild(Capacitor, parentIndex: 0, childIndex: 0)
            .WithChild(InfuseFire, parentIndex: 0 , childIndex: 0)
            .WithChild(TargetPort, parentIndex: 0, childIndex: 0);
        
        SingleSplit =
            singleSplitBuilder
            .Build()
            .AsReadOnly();
    }
}