using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Graphs;

class Graph
{
    public GraphNode? Start { get; set; }

    // dodaj cvor
    public void AddNode(int value)
    {
        var newNode = new GraphNode(value);

        if (Start == null)
        {
            Start = newNode;
            return;
        }

        newNode.Next = Start;
        Start = newNode;
    }

    // dodaj poteg
    public void AddEdge(int value1, int value2, uint weight = 1)
    {
        if (value1 == value2) return;

        if (Start == null)
        {
            return;
        }


        var node1 = Start;
        var node2 = Start;

        while (node1 != null)
        {
            if (node1.Value == value1)
            {
                break;
            }
            node1 = node1.Next;
        }

        while (node2 != null)
        {
            if (node2.Value == value2)
            {
                break;
            }
            node2 = node2.Next;
        }

        if (node1 == null || node2 == null)
        {
            return;
        }


        var newEdge1 = new GraphEdge(weight, node2);
        var newEdge2 = new GraphEdge(weight, node1);

        if (node1.Adj == null)
        {
            node1.Adj = newEdge1;
            return;
        }

        newEdge1.Link = node1.Adj;
        node1.Adj = newEdge1;


        if (node2.Adj == null)
        {
            node2.Adj = newEdge2;
            return;
        }

        newEdge2.Link = node2.Adj;
        node2.Adj = newEdge2;
    }


    // brisi cvor
    // brisi poteg

    // stavi status svim cvorevima
    public void SetAllStatuses(int value)
    {
        if (Start == null) return;
        var curr = Start;

        while (curr != null)
        {
            curr.Status = value;
            curr = curr.Next;
        }
    }


    // printaj
    public void Print()
    {
        if (Start == null) return;

        SetAllStatuses(0);
        var stejk = new StackMy<GraphNode>();

        stejk.Push(Start);
        Start.Status = 1;
        while (stejk.IsEmpty() == false)
        {
            var elem = stejk.Pop();
            if (elem == null) return;

            elem.Status = 2;

            // ob
            System.Console.WriteLine(elem.Value);

            var edz = elem.Adj;

            while (edz != null)
            {
                if (edz.Dest!.Status == 0)
                {
                    edz.Dest!.Status = 1;
                    stejk.Push(edz.Dest);
                }
                
                edz = edz.Link;
            }
        }

    }

}