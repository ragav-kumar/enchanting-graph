// See https://aka.ms/new-console-template for more information

using EnchantingGraph;

Console.WriteLine("Single path");
Console.WriteLine(GraphParser.Parse(InitializerData.SinglePath));

GraphParser splitParser = GraphParser.Parse(InitializerData.SingleSplit);
Console.WriteLine("Single split");
Console.WriteLine(splitParser);

Console.WriteLine("Effects of Path 1 in Single split");
Console.WriteLine(PathInterpreter.Interpret(splitParser.Paths[0]));

Console.WriteLine("Effects of Path 2 in Single split");
Console.WriteLine(PathInterpreter.Interpret(splitParser.Paths[1]));