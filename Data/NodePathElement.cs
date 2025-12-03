namespace EnchantingGraph.Data;

public record NodePathElement
{
    public required INode Node { get; init; }
    public List<INode>? NextNodes { get; init; } = null;
    public List<INode>? AltNextNodes { get; init; } = null;
    
    public List<INode> AllNext() {
        List<INode> next = NextNodes ?? [];
        if (Node.SupportsAltPath && AltNextNodes != null)
        {
            return next.Concat(AltNextNodes).ToList();
        }

        return next;
    }
}