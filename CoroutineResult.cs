using System;
using System.Collections;

public class CoroutineResult : CoroutineResult<object>
{
    public CoroutineResult(object value) : base(value) { }

    public CoroutineResult() : base(value: null) { }

    public static IEnumerator Get<TResult, TError>(IEnumerator routine, Action<TResult> onResult, Action<TError> onError)
    {
        while (routine?.MoveNext() ?? false)
        {
            if (routine.Current is CoroutineResult<TResult> result)
            {
                onResult?.Invoke(result.Value);
                yield break;
            }
            else if (routine.Current is CoroutineError<TError> error)
            {
                onError?.Invoke(error.Value);
                yield break;
            }
            else
            {
                yield return routine.Current;
            }
        }
    }

    public static IEnumerator Get<T>(IEnumerator routine, Action<T> onResult, Action<object> onError) => Get<T, object>(routine, onResult, onError);

    public static IEnumerator Get<T>(IEnumerator routine, Action<T> onResult) => Get<T, object>(routine, onResult, onError: null);

    public static IEnumerator Get(IEnumerator routine, Action<object> onResult, Action<object> onError) => Get<object, object>(routine, onResult, onError);

    public static IEnumerator Get(IEnumerator routine, Action<object> onResult) => Get<object, object>(routine, onResult, onError: null);


}

public class CoroutineResult<T> : IEnumerator
{
    public object Current { get; private set; }

    public T Value { get; private set; }

    public bool MoveNext() => false;

    public void Reset() { }

    public CoroutineResult(T value)
    {
        Value = value;
        Current = this;
    }
}
