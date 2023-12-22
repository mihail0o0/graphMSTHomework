namespace Graphs;

class GraphNode
{
    public int Value { get; set; }
    public GraphNode? Next { get; set; }
    public GraphEdge? Adj { get; set; }

    public GraphNode()
    {
        Value = 0;
        Next = null;
        Adj = null;    
    }
}