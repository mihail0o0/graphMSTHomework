namespace Graphs;

class GraphEdge
{
    public uint Weight { get; set; }
    public GraphNode? Dest { get; set; }
    public GraphEdge? Link { get; set; }

    public GraphEdge()
    {
       Weight = 1;
       Dest = null;
       Link = null; 
    }
    public GraphEdge(uint Weight)
    {
       this.Weight = Weight;
       Dest = null;
       Link = null; 
    }
    public GraphEdge(uint Weight, GraphNode Dest)
    {
       this.Weight = Weight;
       this.Dest = Dest;
       Link = null; 
    }

}