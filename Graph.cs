using System.Collections;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.Versioning;

using Microsoft.VisualBasic;

namespace Graphs;

class Graph : IEnumerable {
    public GraphNode? Start { get; set; }

    // dodaj cvor
    public GraphNode? AddNode(int value) {
        var newNode = new GraphNode(value);

        if (Start == null) {
            Start = newNode;
            return null;
        }

        newNode.Next = Start;
        Start = newNode;
        return newNode;
    }

    // dodaj poteg
    public void AddEdge(int value1, int value2, uint weight = 1) {
        if (value1 == value2) return;

        if (Start == null) {
            return;
        }


        var node1 = Start;
        var node2 = Start;

        while (node1 != null) {
            if (node1.Value == value1) {
                break;
            }
            node1 = node1.Next;
        }

        while (node2 != null) {
            if (node2.Value == value2) {
                break;
            }
            node2 = node2.Next;
        }

        if (node1 == null || node2 == null) {
            return;
        }


        var newEdge1 = new GraphEdge(weight, node2);
        var newEdge2 = new GraphEdge(weight, node1);

        if (node1.Adj == null) {
            node1.Adj = newEdge1;
            goto secondNode;
        }

        newEdge1.Link = node1.Adj;
        node1.Adj = newEdge1;

    secondNode:

        if (node2.Adj == null) {
            node2.Adj = newEdge2;
            return;
        }

        newEdge2.Link = node2.Adj;
        node2.Adj = newEdge2;
    }

    public GraphNode? FindNode(int val1) {
        foreach (GraphNode nod in this) {
            if (nod.Value == val1) return nod;
        }
        return null;
    }

    public GraphEdge? FindEdge(int val1, int val2) {
        var nod1 = Start;

        while (nod1 != null) {
            if (nod1.Value == val1) break;
            nod1 = nod1.Next;
        }

        if (nod1 == null) return null;

        foreach (GraphEdge edz in nod1!) {
            if (edz.Dest!.Value == val2) {
                return edz;
            }
        }

        return null;
    }


    // brisi cvor
    public void RemoveNode(int value) {
        if (Start == null) {
            return;
        }

        var toDel = Start;
        var prevToDel = Start;

        while (toDel != null) {
            if (toDel.Value == value) break;
            prevToDel = toDel;
            toDel = toDel.Next;
        }

        if (toDel == null) return;

        // delete reflections
        foreach (GraphEdge edz in toDel) {
            var reflectionStart = edz.Dest!.Adj;
            var reflection = reflectionStart;
            var preFlection = reflectionStart;

            while (reflection != null) {
                if (reflection.Dest == toDel) {
                    if (reflection == reflectionStart) {
                        if (reflectionStart.Link == null) {
                            edz.Dest!.Adj = null;
                            break;
                        }

                        var tmp = reflectionStart.Link;
                        reflectionStart.Link = null;
                        reflectionStart = tmp;
                        reflection = tmp;
                        preFlection = tmp;
                        continue;
                    }

                    preFlection!.Link = reflection.Link;
                    reflection.Link = null;
                    reflection = preFlection.Link;
                    continue;
                }

                preFlection = reflection;
                reflection = reflection.Link;
            }
        }

        PrintAll();

        toDel.Adj!.Link = null;
        toDel.Adj = null;

        if (toDel == Start) {
            var tmp = Start.Next;
            Start.Next = null;
            Start = tmp;
            return;
        }

        prevToDel.Next = toDel.Next;
        toDel.Next = null;
    }

    // stavi status svim cvorevima
    public void SetAllStatuses(int value) {
        if (Start == null) return;
        var curr = Start;

        while (curr != null) {
            curr.Status = value;
            curr = curr.Next;
        }
    }

    // printaj
    public void Print() {
        if (Start == null) return;

        SetAllStatuses(0);
        var stejk = new StackMy<GraphNode>();

        stejk.Push(Start);
        Start.Status = 1;
        while (stejk.IsEmpty() == false) {
            var elem = stejk.Pop();
            if (elem == null) return;
            elem.Status = 2;

            System.Console.WriteLine(elem.Value);

            foreach (GraphEdge edz in elem) {
                if (edz.Dest!.Status == 0) {
                    edz.Dest!.Status = 1;
                    stejk.Push(edz.Dest);
                }
            }
        }
    }

    public void PrintAll() {
        foreach (GraphNode nod in this) {
            System.Console.Write($"{nod.Value} -> ");
            foreach (GraphEdge edz in nod) {
                System.Console.Write($"to {edz.Dest?.Value} : {edz.Weight} | ");
            }

            System.Console.WriteLine();
        }
    }

    public Graph? Prime(int startingNode) {

        Graph MST = new();
        BinomialHeap bh = new();
        //Dictionary<Tuple<GraphNode, GraphNode>, BinomialNode> edgesInHeap = new();
        Dictionary<GraphNode, BinomialNode> inHeap = new();

        if (Start == null) return null;

        SetAllStatuses(0);

        foreach (GraphNode nod in this) {
            System.Console.WriteLine(nod.Value);
            if (nod.Value == startingNode) {
                var inserted = bh.Insert(0, nod);
                inHeap.Add(nod, inserted);
            }
            else {
                var inserted = bh.Insert(int.MaxValue, nod);
                inHeap.Add(nod, inserted);
            }

            MST.AddNode(nod.Value);
        }

        while (bh.IsEmpty() == false) {
            var bhExt = bh.ExtractMinimum();
            GraphNode curMin = bhExt.value!;
            int graphWeight = bhExt.key;
            // System.Console.WriteLine($"{curMin.Value}: ");
            //bh.PrintHeap();
            // System.Console.WriteLine("+");

            foreach (GraphEdge edz in curMin) {
                BinomialNode? nodeInBh = inHeap!.GetValueOrDefault(edz.Dest);

                if (nodeInBh == null) continue;
                if (edz.Dest!.Status == 0 && edz.Weight < nodeInBh.key) {
                    bh.DecreasePriority(nodeInBh, (int)edz.Weight);

                    var nodeInMst = MST.FindNode(edz.Dest!.Value);

                    nodeInMst!.Adj ??= new GraphEdge();
                    nodeInMst.Adj.Dest = MST.FindNode(curMin.Value);
                    nodeInMst.Adj.Weight = edz.Weight;
                    // MST.PrintAll();
                    // System.Console.WriteLine("---");
                }
                else {
                    // System.Console.WriteLine($"{edz.Weight} > {nodeInBh.key}, {edz.Dest.Value}");
                    // System.Console.WriteLine("----");
                }
            }
        }

        return MST;
    }

    public IEnumerator GetEnumerator() {
        return new GraphEnumerator(Start);
    }

    public class GraphEnumerator : IEnumerator {
        public GraphNode? Start;
        public GraphNode? Current { get; private set; }

        object IEnumerator.Current => Current!;

        public GraphEnumerator(GraphNode? Start) {
            this.Start = Start;
            Current = null;
        }

        public bool MoveNext() {
            if (Start == null) return false;

            if (Current == null) {
                Current = Start;
                return true;
            }
            if (Current.Next == null) return false;

            Current = Current.Next;
            return true;
        }

        public void Reset() {
            Current = Start;
        }

    }
}