using EnchantingGraph.Data;

namespace EnchantingGraph.Graph;

public class GraphBuilder
{
    private HashSet<NodePathElement> Nodes { get; } = [];

    private NodePathElement? lastAdded_ = null;

    public GraphBuilder Add(NodeBase node, List<(NodeBase parentNode, int parentIndex, int childIndex)>? parents = null)
    {
        NodePathElement newNode = new(node);
        if (!Nodes.Add(newNode))
        {
            throw new ArgumentException($"Node {node} already exists");
        }

        if (parents is not null)
        {
            for (int i = 0; i < parents.Count; i++)
            {
                (NodeBase parentNode, int parentIndex, int childIndex) = parents[i];
                NodePathElement inputElement = Nodes.Single(o => o.Node.Equals(parents[i].parentNode));
                
                newNode.ConnectInput(parentNode, childIndex);
                inputElement.ConnectOutput(node, parentIndex);
            }
        }
        lastAdded_ = newNode;

        return this;
    }

    public GraphBuilder Add(NodeBase node, NodeBase parent, int parentIndex, int childIndex) =>
        Add(node, [(parent, parentIndex, childIndex)]);

    public GraphBuilder WithChild(NodeBase node, int parentIndex, int childIndex)
    {
        if (lastAdded_ is null)
        {
            throw new InvalidOperationException();
        }
        return Add(node, [(lastAdded_.Node, parentIndex, childIndex)]);
    }
    
    public HashSet<NodePathElement> Build() => Nodes;
}