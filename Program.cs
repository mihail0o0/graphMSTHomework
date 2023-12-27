namespace Graphs;
class Program {
    static void Main(string[] args) {
        Graph graph = new();

        for (int i = 0; i <= 8; i++) {
            graph.AddNode(i);
        }
        graph.AddEdge(0, 1, 4);
        graph.AddEdge(1, 2, 8);
        graph.AddEdge(2, 3, 7);
        graph.AddEdge(3, 4, 9);
        graph.AddEdge(4, 5, 10);
        graph.AddEdge(5, 6, 2);
        graph.AddEdge(6, 7, 1);
        graph.AddEdge(7, 0, 8);
        graph.AddEdge(1, 7, 11);
        graph.AddEdge(2, 8, 2);
        graph.AddEdge(2, 5, 4);
        graph.AddEdge(3, 5, 14);
        graph.AddEdge(6, 8, 6);
        graph.AddEdge(7, 8, 7);

        graph.PrintAll();
        System.Console.WriteLine("------------------------------");
        var newGraph = graph.Prime();
        newGraph.PrintAll();

        //g.PrintAll();

        // BinomialHeap bh = new();
        // bh.Insert(10);

        // Dictionary<Tuple<GraphNode, GraphNode>, BinomialNode> EdgesInHeap = new();

        // EdgesInHeap.Add(Tuple.Create(g.Start, g.Start.Next), bh.Minimum());

        // if(EdgesInHeap.ContainsKey((g.Start, g.Start.Next))){
        // System.Console.WriteLine(EdgesInHeap.TryGetValue(new EdgeWrapper(g.Start, g.Start.Next), out BinomialNode val));
        // System.Console.WriteLine(val);
        // }
        // else{
        // System.Console.WriteLine("Jao mame");
        // }

    }
}
