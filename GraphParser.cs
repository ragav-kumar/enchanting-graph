using EnchantingGraph.Data;

namespace EnchantingGraph;

public class GraphParser
{
    public List<NodePathElement> Graph { get; }
    public List<List<INode>> Paths { get; }

    public GraphParser(List<NodePathElement> graph)
    {
        Graph = graph;
        if (!TryValidateInvariants(out string? errorMessage))
        {
            throw new InvalidDataException(errorMessage);
        }
        Paths = [];
        ExtractPaths(Graph);

        if (Paths.Count == 0)
        {
            throw new InvalidDataException("The enchantment graph contains no paths.");
        }
    }

    private void ExtractPaths(List<NodePathElement> graph)
    {
        NodePathElement source = graph.Single(o => o.Node is SourceNode);
        List<INode> currentPath = [];
        HashSet<INode> currentSet = [];

        void Dfs(NodePathElement pathElement)
        {
            // Cycle = dead end
            if (currentSet.Contains(pathElement.Node))
            {
                return;
            }

            if (pathElement.Node is PortNode)
            {
                Paths.Add([..currentPath, pathElement.Node]);
                return;
            }

            foreach (INode node in pathElement.AllNext())
            {
                NodePathElement nextPathElement = graph.Single(o => o.Node == node);
                Dfs(nextPathElement);
            }

            currentPath.RemoveAt(currentPath.Count - 1);
            currentSet.Add(pathElement.Node);
        }

        Dfs(source);
    }

    private bool TryValidateInvariants(out string? errorMessage)
    {
        // There must be exactly one source
        int sourceCount = Graph.Count(o => o.Node is SourceNode);
        switch (sourceCount)
        {
            case 0:
                errorMessage = "The enchantment is missing a source node.";
                return false;
            case > 1:
                errorMessage = "The enchantment can have at most one source node.";
                return false;
        }
        
        // There must be at least one port
        int portCount = Graph.Count(o => o.Node is PortNode);
        if (portCount == 0)
        {
            errorMessage = "The enchantment must have at least one port node.";
            return false;
        }
        
        // There must be at least one path that starts at the source and ends at a port.
        // However, this can't be tested here.
        errorMessage = null;
        return true;
    }
}