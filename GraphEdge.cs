namespace Graphs;

class GraphEdge
{
    public uint Weight { get; set; }
    public GraphNode? Next { get; set; }
    public GraphEdge? Link { get; set; }

    public GraphEdge()
    {
       Weight = 1;
       Next = null;
       Link = null; 
    }
}