using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Graphs;
class BinomialHeap {
    public BinomialNode? head;
    public BinomialHeap() {
        head = null;
    }
    public void Insert(BinomialNode x) {
        BinomialHeap newHeap = new BinomialHeap();
        newHeap.head = x;
        this.head = Union(newHeap).head;
    }
    public BinomialNode Insert(int x) {
        BinomialNode newNode = new(x);
        this.Insert(newNode);
        return newNode;
    }

    public BinomialNode Insert(int x, GraphNode node) {
        BinomialNode newNode = new(x, node);
        this.Insert(newNode);
        return newNode;
    }

    public bool IsEmpty() {
        return this.head == null;
    }

    public BinomialNode Minimum() {
        if (head == null)
            throw new Exception("Nemoguce uzeti minimum iz praznog heap-a");

        BinomialNode? current = head.next;

        BinomialNode min = head;
        while (current != null) {
            if (current.key < min.key) {
                min = current;
            }
            current = current.next;
        }
        return min;
    }
    public BinomialNode ExtractMinimum() {
        BinomialNode min = this.Minimum();

        if (min == head && min.next == null) {
            this.head = null;
        }

        if (min.previous == null) {
            this.head = min.next;
            if (min.next != null) {
                min.next.previous = null;
            }
        }
        else {
            min.previous.next = min.next;
            if (min.next != null) {
                min.next.previous = min.previous;
            }
        }

        min.children.Reverse();
        BinomialHeap newHeap = new BinomialHeap();

        BinomialNode? previous = null;
        foreach (BinomialNode child in min.children) {
            child.previous = previous;
            if (previous == null) {
                newHeap.head = child;
            }
            else {
                previous.next = child;
            }
            child.next = null;
            child.parent = null;
            previous = child;
        }
        // System.Console.WriteLine("Bifor Union");
        // this.PrintHeap();
        // System.Console.WriteLine();
        if (this.head != null || newHeap.head != null)
            head = this.Union(newHeap).head;


        // System.Console.WriteLine("After Union");
        // this.PrintHeap();
        // System.Console.WriteLine();
        return min;
    }
    public void IncreasePriority(int newKey) {
        throw new NotImplementedException();
    }
    public void DekrizMiDer(BinomialNode toDecrease, int newKey, Dictionary<GraphNode, BinomialNode> inHeap) {
        if (toDecrease.key < newKey) {
            Console.WriteLine("Kljuc je vec manji");
            return;
        }
        var toRet = toDecrease;
        toDecrease.key = newKey;

        // Perform necessary adjustments in the heap structure
        BinomialNode? current = toDecrease;
        BinomialNode? parrent = current.parent;

        while (parrent != null && current.key < parrent.key) {
            int tmpKey = parrent.key;
            parrent.key = current.key;
            current.key = tmpKey;

            GraphNode? tmpValue = parrent.value;
            parrent.value = current.value;
            current.value = tmpValue;

            BinomialNode inHeapTmp = inHeap[current.value];
            inHeap[current.value] = inHeap[parrent.value];
            inHeap[parrent.value] = inHeapTmp;

            // Move upward in the heap
            current = parrent;
            parrent = current.parent;
        }
    }
    public static BinomialHeap Merge(BinomialHeap H1, BinomialHeap H2) {
        BinomialHeap newHeap = new BinomialHeap();

        int currentDegree = 0;

        BinomialNode? lastAdded = null;
        BinomialNode? nextFromH1 = H1.head;
        BinomialNode? nextFromH2 = H2.head;

        while (nextFromH1 != null || nextFromH2 != null) {
            while (nextFromH1 != null && nextFromH1.degree == currentDegree) {
                if (lastAdded == null) {
                    newHeap.head = nextFromH1;
                }
                else {
                    lastAdded.next = nextFromH1;
                }
                nextFromH1.previous = lastAdded;
                lastAdded = nextFromH1;
                nextFromH1 = nextFromH1.next;
            }
            while (nextFromH2 != null && nextFromH2.degree == currentDegree) {
                if (lastAdded == null) {
                    newHeap.head = nextFromH2;
                }
                else {
                    lastAdded.next = nextFromH2;
                }
                nextFromH2.previous = lastAdded;
                lastAdded = nextFromH2;
                nextFromH2 = nextFromH2.next;
            }
            currentDegree++;
        }

        // System.Console.WriteLine("After mrdz");
        // H1.PrintHeap();
        // H2.PrintHeap();
        return newHeap;
    }
    public BinomialHeap Union(BinomialHeap other) {
        BinomialHeap mergedHeap = Merge(this, other);
        if (mergedHeap.head == null)
            throw new Exception("Neuspesno spajanje heap-ova");


        BinomialNode? current = mergedHeap.head;
        while (current.next != null) {
            if ((current.degree != current.next.degree) || (current.next.next != null && current.next.next.degree == current.degree)) {
                current = current.next;
                continue;
            }
            BinomialNode? theOneAfterChaos = current.next.next;
            BinomialNode? theOneBeforeChaos = current.previous;
            //System.Console.WriteLine("Bifor mrdz");
            //mergedHeap.PrintHeap();


            BinomialNode newRoot = BinomialTree.Merge(current, current.next);
            //System.Console.WriteLine("After mrdz");
            //newRoot.Print();
            //System.Console.WriteLine();
            if (theOneBeforeChaos == null) {
                mergedHeap.head = newRoot;
                newRoot.previous = null;
            }
            else {
                theOneBeforeChaos.next = newRoot;
                newRoot.previous = current.previous;
            }

            newRoot.next = theOneAfterChaos;
            if (theOneAfterChaos != null) {
                theOneAfterChaos.previous = newRoot;
            }

            current = newRoot;
        }

        return mergedHeap;
    }
    public void Print() {
        if (head == null)
            throw new Exception("Pokusaj stampanja praznog heap-a");

        Queue<BinomialNode?> queue = new();

        BinomialNode? currentRoot = head;
        queue.Enqueue(currentRoot);
        queue.Enqueue(null);
        while (currentRoot != null) {
            while (queue.Count > 0) {
                BinomialNode? current = queue.Dequeue();
                if (current == null) {
                    Console.WriteLine();
                }
                else {
                    Console.Write($"{current.key} ");
                    foreach (BinomialNode child in current.children) {
                        queue.Enqueue(child);
                    }
                    if (queue.Peek() == null) {
                        queue.Enqueue(null);
                    }
                }
            }
            currentRoot = currentRoot.next;
            queue.Enqueue(currentRoot);
            queue.Enqueue(null);

        }
    }
    public void PrintHeap() {
        var curr = head;

        while (curr != null) {
            PrintTree(curr);
            curr = curr.next;
        }
    }

    public void PrintTree(BinomialNode? root = null, string indent = "", bool last = true) {
        if (head == null) return;
        root ??= head;

        Console.Write(indent);
        if (last) {
            Console.Write("\\-");
            indent += "  ";
        }
        else {
            Console.Write("|-");
            indent += "| ";
        }

        Console.WriteLine($"{root.key} {root.value!.Value}");

        for (int i = 0; i < root.children.Count; i++) {
            PrintTree(root.children[i], indent, i == root.children.Count - 1);
        }

    }
}