using System.Numerics;

namespace Graphs;
class Program {
    static void Main(string[] args) {
        Graph graph = new();

        for (int i = 0; i <= 8; i++) {
            graph.AddNode(i);
        }
        graph.AddEdge(0, 1, 4);
        graph.AddEdge(0, 7, 8);
        graph.AddEdge(1, 7, 11);
        graph.AddEdge(1, 2, 8);
        graph.AddEdge(7, 6, 1);
        graph.AddEdge(7, 8, 7);
        graph.AddEdge(2, 8, 2);
        graph.AddEdge(8, 6, 6);
        graph.AddEdge(2, 3, 7);
        graph.AddEdge(2, 5, 4);
        graph.AddEdge(6, 5, 2);
        graph.AddEdge(3, 5, 14);
        graph.AddEdge(3, 4, 9);
        graph.AddEdge(5, 4, 10);

        graph.PrintAll();
        System.Console.WriteLine("------------------------------");
        var newGraph = graph.Prime(0);
        newGraph.PrintAll();


        // BinomialHeap bh = new();
        // bh.Insert(3);
        // bh.Insert(1);
        // bh.Insert(5);

        // bh.PrintHeap();

        // System.Console.WriteLine(bh.ExtractMinimum().key);
        // System.Console.WriteLine();
        // bh.PrintHeap();
        // System.Console.WriteLine(bh.ExtractMinimum().key);
        // System.Console.WriteLine();
        // bh.PrintHeap();
        // System.Console.WriteLine(bh.ExtractMinimum().key);
        // System.Console.WriteLine();
        // bh.PrintHeap();
    }
}
