namespace Graphs;

class LinkedLista<T>
{
    public LLNode<T>? Head { get; set; }

    public LinkedLista()
    {
        Head = null;
    }
    public void AddHead(T newValue)
    {
        AddHead(new LLNode<T>(newValue));
    }


    public void AddHead(LLNode<T> newNode)
    {
        if (this.Head == null)
        {
            Head = newNode;
            return;
        }

        newNode.Next = Head;
        Head = newNode;
    }
    public void AddTail(T newValue)
    {
        AddTail(new LLNode<T>(newValue));
    }

    public void AddTail(LLNode<T> newNode)
    {
        if (this.Head == null)
        {
            Head = newNode;
            return;
        }

        LLNode<T> curr = Head;
        while (curr.Next != null)
        {
            curr = curr.Next;
        }

        curr.Next = newNode;
    }

    public void RemoveHead(){
        if(this.Head == null){
            return;
        }

        var tmp = Head;
        Head = Head.Next;
        tmp.Next = null;
    }

    public void RemoveTail(){
        if(this.Head == null){
            return;
        }

        if(this.Head.Next == null){
            this.Head = null;
            return;
        }
        
        var curr = Head;
        var prev = Head;


        while(curr.Next != null){
            prev = curr;
            curr = curr.Next;
        }

        prev.Next = null;
    }

    public void RemoveNode(T Data)
    {
        if (Head == null) return;
        LLNode<T>? toDelete = Head;
        LLNode<T>? prev = Head;


        while (toDelete != null)
        {
            if (EqualityComparer<T>.Default.Equals(toDelete.Data, Data))
            {
                RemoveNode(toDelete, prev);
                return;
            }
            prev = toDelete;
            toDelete = toDelete.Next;
        }
    }

    public void RemoveNode(LLNode<T> toDelete, LLNode<T> prev)
    {
        if (toDelete == Head)
        {
            var tmp = Head;
            Head = Head.Next;
            tmp.Next = null;
            return;
        }

        prev.Next = toDelete.Next;
        toDelete.Next = null;
    }

    public void Print()
    {
        LLNode<T>? curr = Head;

        while (curr != null)
        {
            System.Console.WriteLine(curr.Data);
            curr = curr.Next;
        }
    }
}