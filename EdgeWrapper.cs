
using Graphs;

class EdgeWrapper
{
    public GraphNode EdgeFrom { get; set; }
    public GraphNode EdgeTo { get; set; }

    public EdgeWrapper(GraphNode from, GraphNode to)
    {
        this.EdgeFrom = from;
        this.EdgeTo = to;
    }


}