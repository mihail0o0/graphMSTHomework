using System.ComponentModel.DataAnnotations;

namespace Graphs;

class StackMy<T>
{
    public T[] Arr { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }


    public StackMy()
    {
        Size = 100;
        Arr = new T[Size];
        Count = 0;
    }

    public StackMy(int Length)
    {
        this.Size = Length;
        this.Arr = new T[Length];   
        Count = 0;
    }


    public void Push(T value){
        if(IsFull()) return;
        Arr[Count++] = value;  
    }

    public T? Pop(){
        if(IsEmpty()) return default;
        return Arr[--Count];
    }

    public T Peek(){
        return Arr[Count - 1];
    }

    public bool IsEmpty(){
        if(Count == 0){
            return true;
        }

        return false;
    }

    public bool IsFull(){
        if(Count == Size) return true;
        return false;
    }
}