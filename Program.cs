// See https://aka.ms/new-console-template for more information

using EnchantingGraph.Graph;

const int numTicks = 100;
const int breakLine = 50;

Console.WriteLine("Single path");
RunSimulation(InitializerData.SinglePath, "single path");
Console.WriteLine();
Console.WriteLine("Single split");
RunSimulation(InitializerData.SingleSplit, "single split");
return;

void RunSimulation(IEnumerable<NodePathElement> graph, string name)
{
    Simulator simulator = new(graph);
    Console.WriteLine($"Simulating {name}...");
    for (int i = 1; i <= numTicks; i++)
    {
        simulator.Tick(i);
        Console.Write(".");
        if (i % breakLine == 0)
        {
            Console.WriteLine();
        }
    }
    Console.WriteLine("Simulation completed.");
    Console.WriteLine($"Simulation ran for {numTicks} ticks.");
    Console.WriteLine(simulator.Results());
}