using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace Graphs;

class GraphNode : IEnumerable
{
    public int Value { get; set; }
    public int Status { get; set; }
    public GraphNode? Next { get; set; }
    public GraphEdge? Adj { get; set; }

    public GraphNode()
    {
        Value = 0;
        Next = null;
        Adj = null;    
        Status = 0;
    }
    public GraphNode(int value)
    {
        this.Value = value;
        Next = null;
        Adj = null;    
        Status = 0;
    }

    public IEnumerator GetEnumerator() {
        return new GraphNodeEnumerator(Adj);
    }

    public class GraphNodeEnumerator : IEnumerator {
        public GraphEdge? Adj { get; private set; }
        public GraphEdge? Current { get; set; }

        public GraphNodeEnumerator(GraphEdge? adj)
        {
            Adj = adj;
            Current = null;
        }

        object IEnumerator.Current => Current!;

        public bool MoveNext() {
            if(Adj == null) return false;

            if(Current == null) {
                Current = Adj;
                return true;
            }

            if(Current.Link == null) return false;

            Current = Current.Link;
            return true;
        }

        public void Reset() {
            Current = Adj;
        }
    }
}