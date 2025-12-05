namespace EnchantingGraph.Data;

public record NodePathElement
{
    public required NodeBase Node { get; init; }
    public List<NodeBase>? InputNodes { get; init; } = null;
    public List<NodeBase>? OutputNodes { get; init; } = null;
}