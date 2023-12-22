namespace Graphs;
class Program
{
    static void Main(string[] args)
    {
        Graph g = new Graph();

        g.AddNode(2);
        g.AddNode(3);
        g.AddNode(4);
        g.AddNode(5);
        g.AddNode(6);
        g.AddNode(1);

        g.AddEdge(1, 2);
        g.AddEdge(2, 3);
        g.AddEdge(3, 6);
        g.AddEdge(6, 4);

        g.Print();
    }
}
