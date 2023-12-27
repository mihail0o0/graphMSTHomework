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
}