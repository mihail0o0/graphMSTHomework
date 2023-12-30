using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs;

class BinomialNode {
    public int key;
    public GraphNode? value;
    public BinomialNode? parent;
    public BinomialNode? previous;
    public BinomialNode? next;
    public List<BinomialNode> children;
    public int degree {
        get {
            return children.Count();
        }
    }
    public BinomialNode(int key) {
        this.key = key;
        this.value = null;
        parent = null;
        previous = null;
        next = null;
        children = new();
    }
    public BinomialNode(int key, GraphNode? value) {
        this.key = key;
        this.value = value;
        parent = null;
        previous = null;
        next = null;
        children = new();
    }
    public void Print(BinomialNode? root = null, string indent = "", bool last = true) {
        if (this == null) return;
        root ??= this;

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
            Print(root.children[i], indent, i == root.children.Count - 1);
        }

    }
}