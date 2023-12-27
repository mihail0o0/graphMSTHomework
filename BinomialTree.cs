using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs;
class BinomialTree {
    public static BinomialNode Merge(BinomialNode root1, BinomialNode root2) {
        BinomialNode parent;
        BinomialNode child;
        if (root1.key < root2.key) {
            parent = root1;
            child = root2;
        }
        else {
            parent = root2;
            child = root1;
        }

        child.parent = parent;
        child.next = null;
        child.previous = null;
        parent.children.Insert(0, child);

        return parent;
    }

}