namespace Graphs;

class LLNode<T> : IComparable<T>
{
    public T? Data { get; set; }
    public LLNode<T>? Next { get; set; }

    public LLNode(T Data)
    {
        this.Data = Data;
    }

    public int CompareTo(T? other)
    {
        if(EqualityComparer<T>.Default.Equals(Data, other)) return 1;
        return -1;
    }
}