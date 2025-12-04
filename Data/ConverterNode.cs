namespace EnchantingGraph.Data;

/// <summary>
/// Converts InElement to OutElement at the given efficiency
/// </summary>
public record ConverterNode : INode
{
    public required Element InElement { get; init; }
    public required Element OutElement { get; init; }
    public required float Efficiency { get; init; }
    public bool SupportsAltPath => false;
}