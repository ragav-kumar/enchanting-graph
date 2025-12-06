using System.Text;
using EnchantingGraph.Data;

namespace EnchantingGraph.Graph;

public class Simulator
{
    public HashSet<NodePathElement> Graph { get; }
    private List<Packet> emitted_;
    private NodePathElement source_;
    
    public Simulator(IEnumerable<NodePathElement> data)
    {
        Graph = new HashSet<NodePathElement>(data);
        emitted_ = [];
        Validate();
        source_ = Graph.Single(o => o.Node is SourceNode);
    }

    private void Validate()
    {
        // There must be exactly one source
        int sourceCount = Graph.Count(o => o.Node is SourceNode);
        if (sourceCount != 1)
        {
            throw new InvalidNodePlacementException("There must be exactly one source node");
        }
        
        // There must be at least one port
        int portCount = Graph.Count(o => o.Node is PortNode);
        if (portCount < 1)
        {
            throw new InvalidNodePlacementException("There must be at least one port node");
        }

        // There must be at least one path from source to port.
        if (!PathExists())
        {
            throw new InvalidNodePlacementException("There must be at least one path from source to port.");
        }
    }
    
    private static bool IsPort(NodePathElement node) => node.Node is PortNode;

    private bool PathExists()
    {
        HashSet<NodePathElement> visited = [];
        Stack<NodePathElement> stack = [];
        
        NodePathElement source = Graph.Single(o => o.Node is SourceNode);
        
        stack.Push(source);
        while (stack.Count > 0)
        {
            NodePathElement current = stack.Pop();
            if (IsPort(current))
            {
                return true;
            }

            if (!visited.Add(current))
            {
                continue;
            }

            foreach (NodeBase? node in current.OutputNodes.Where(n => n is not null))
            {
                if (node is null)
                {
                    continue;
                }
                NodePathElement element = Graph.Single(o => o.Node.Equals(node));
                if (!visited.Contains(element))
                {
                    stack.Push(element);
                }
            }
        }

        return false;
    }

    public void Tick()
    {
        Queue<NodePathElement> queue = [];
        HashSet<NodeBase> visited = [];
        Dictionary<NodeBase, Dictionary<int, Packet>> pendingInputs = [];
        
        // Always start from the source.
        queue.Enqueue(source_);
        while (queue.Count > 0)
        {
            NodePathElement current = queue.Dequeue();
            
            // If this node has already been visited, skip.
            if (!visited.Add(current.Node))
            {
                continue;
            }

            // If this node has unvisited parents, put it back at the back of the queue.
            if (current.InputNodes.Any(n => n is not null && !visited.Contains(n)))
            {
                queue.Enqueue(current);
                continue;
            }
            
            // We're tracking the inputs to this node in the pendingInputs dictionary.
            if (!pendingInputs.TryGetValue(current.Node, out Dictionary<int, Packet>? packets))
            {
                packets = [];
            }
            // Simulate
            Dictionary<int, Packet>? result = current.Node.Simulate(packets);
            
            if (result is not null)
            {
                // If the result has its port value set, it is instead redirected into the emitted data.
                List<Packet> emittable = result.Values.Where(o => o.Port is not null).ToList();
                if (emittable.Count > 0)
                {
                    emitted_.AddRange(emittable);
                    continue;
                }
                
                // Construct the target dicts by iterating over output nodes
                for (int i = 0; i < current.OutputNodes.Count; i++)
                {
                    NodeBase? outputNode = current.OutputNodes[i];
                    if (outputNode is null)
                        continue;
                    if (!result.TryGetValue(i, out Packet packet))
                        throw new InvalidOperationException("Simulation emitted a packet on a disconnected wire.");
                    
                    NodePathElement outputElement = Graph.Single(o => o.Node.Equals(outputNode));
                    int index = outputElement.InputNodes.IndexOf(current.Node);
                    if (!pendingInputs.TryGetValue(outputNode, out Dictionary<int, Packet>? inputPackets))
                    {
                        inputPackets = [];
                    }
                    inputPackets[index] = packet;
                    pendingInputs[outputNode] = inputPackets;
                }
            }
            
            // Enqueue children
            foreach (NodeBase? childNode in current.OutputNodes)
            {
                if (childNode is null)
                    continue;
                NodePathElement childElement = Graph.Single(o => o.Node.Equals(childNode));
                queue.Enqueue(childElement);
            }
        }
        
    }

    public string Results()
    {
        StringBuilder builder = new();
        builder.AppendLine($"Emitted {emitted_.Count} packet(s).");
        builder.AppendLine();
        if (emitted_.Count > 0)
        {
            List<string> packetStrings = emitted_
                .Select(o => o.ToString())
                .ToList();
            
            IEnumerable<(string message, int count)> collapsed = Collapse(packetStrings);
            foreach ((string message, int count) in collapsed)
            {
                builder.AppendLine(message);
                if (count > 1)
                {
                    builder.AppendLine($"    (Repeated {count} times)");
                }
            }
        }
        
        return builder.ToString();
    }

    private static IEnumerable<(string message, int count)> Collapse(List<string> messages)
    {
        using List<string>.Enumerator it = messages.GetEnumerator();
        
        if (!it.MoveNext())
            yield break;

        string current = it.Current;
        int count = 1;

        while (it.MoveNext())
        {
            if (it.Current == current)
            {
                count++;
            }
            else
            {
                yield return (current, count);
                current = it.Current;
                count = 1;
            }
        }

        // emit last run
        yield return (current, count);

    }
}