using EnchantingGraph.Data;
using EnchantingGraph.Effects;

namespace EnchantingGraph;

public class PathInterpreter
{
    public List<INode> Path { get; }
    public Enchantment? PathEnchantment { get; private set; }

    private PathInterpreter(List<INode> path)
    {
        Path = path;
        PathEnchantment = ComputeEnchantment(path);
    }

    public static PathInterpreter Interpret(List<INode> path) => new(path);

    private static Enchantment? ComputeEnchantment(List<INode> path)
    {
        Enchantment enchantment = new();
        
        foreach (INode node in path)
        {
            if (!node.TryAppend(enchantment))
            {
                return null;
            }
        }
        
        return enchantment;
    }
}