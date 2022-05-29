using System.Collections;

public class CoroutineError : CoroutineError<object>
{
    public CoroutineError(object value) : base(value) { }

    public CoroutineError() : base(value: null) { }
}

public class CoroutineError<T> : IEnumerator
{
    public object Current { get; private set; }

    public T Value { get; private set; }

    public bool MoveNext() => false;

    public void Reset() { }

    public CoroutineError(T value)
    {
        Value = value;
        Current = this;
    }
}
