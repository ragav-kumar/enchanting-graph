// See https://aka.ms/new-console-template for more information

using EnchantingGraph;


GraphParser parser = new(InitializerData.SinglePath);
Console.WriteLine(parser.Graph);