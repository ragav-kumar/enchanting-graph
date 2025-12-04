namespace EnchantingGraph.Data;

/// <summary>
/// Acts like two trigger nodes with a capacitor in between.
/// Keyword triggers charge up, and the effect is discharged on charge completion.
/// Power scales with charge time.
/// </summary>
public record FinisherNode : TriggerNode
{
    public required float ChargeTime { get; init; }
}