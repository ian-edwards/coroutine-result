# coroutine-result

```cs
using System.Collections;
using UnityEngine;

public class CoroutineResultBehaviour : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CoroutineResult.Get<int>(CountCharactersRoutine("hello world"),
            onResult: r => Debug.Log($"result={r}")));

        StartCoroutine(CoroutineResult.Get<int, string>(CountCharactersRoutine(null),
            onResult: r => Debug.Log($"result={r}"),
            onError: e => Debug.Log($"error={e}")));
    }

    static IEnumerator CountCharactersRoutine(string s)
    {
        if (s is null) yield return new CoroutineError<string>("invalid null");
        yield return new CoroutineResult<int>(s.Length);
    }
}
```
