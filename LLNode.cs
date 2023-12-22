namespace Graphs;

class LLNode<T>
{
    public T? Data { get; set; }
    public LLNode<T>? Next { get; set; }

    public LLNode(T Data)
    {
        this.Data = Data;
    }
}