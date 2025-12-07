using EnchantingGraph.Data;

namespace EnchantingGraph.Graph;

public class NodePathElement
{
    public NodeBase Node { get; }
    public FixedList<NodeBase?> InputNodes { get; }
    public FixedList<NodeBase?> OutputNodes { get; }

    public NodePathElement(NodeBase node)
    {
        Node = node;
        InputNodes = new FixedList<NodeBase?>(node.ConnectedInputs.Count);
        OutputNodes = new FixedList<NodeBase?>(node.ConnectedOutputs.Count);
    }

    public override string ToString()
    {
        int inCount = InputNodes.Count(o => o != null);
        int outCount = OutputNodes.Count(o => o != null);
        return $"{Node} [{inCount} in, {outCount} out]";
    }

    public void ConnectInput(NodeBase node, int index)
    {
        if (index < 0 || index >= InputNodes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (Node.ConnectedInputs[index])
        {
            throw new InvalidNodePlacementException($"Input {index} is already connected");
        }
        InputNodes[index] = node;
        Node.ConnectedInputs[index] = true;
    }

    public void ConnectOutput(NodeBase node, int index)
    {
        if (index < 0 || index >= OutputNodes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (Node.ConnectedOutputs[index])
        {
            throw new InvalidNodePlacementException($"Output {index} is already connected");
        }
        OutputNodes[index] = node;
        Node.ConnectedOutputs[index] = true;
    }

    public virtual bool Equals(NodePathElement? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return other.Node.Equals(Node);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Node);
    }
}