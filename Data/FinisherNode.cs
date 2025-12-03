namespace EnchantingGraph.Data;

public record FinisherNode : TriggerNode
{
    public required float ChargeTime { get; init; }
}