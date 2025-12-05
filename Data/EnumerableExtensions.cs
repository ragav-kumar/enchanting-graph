namespace EnchantingGraph.Data;

public static class EnumerableExtensions
{
    public static ElementDictionary Sum(this IEnumerable<ElementDictionary> source)
    {
        ElementDictionary sum = new();
        foreach (ElementDictionary dict in source)
        {
            sum += dict;
        }
        return sum;
    }

    public static Packet Sum(this Dictionary<int, Packet> inputs)
    {
        ElementDictionary elements = inputs.Select(o => o.Value.Elements).Sum();
        return new Packet
        {
            Elements = elements,
            IsBurst = inputs.Any(o => o.Value.IsBurst),
            Port = inputs.FirstOrDefault(o => o.Value.Port != null).Value.Port,
            Effect = inputs.FirstOrDefault(o => o.Value.Effect != null).Value.Effect,
            Keyword = inputs.FirstOrDefault(o => o.Value.Keyword != null).Value.Keyword,
        };
    }
}