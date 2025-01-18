using UnityEditor;
using System.Collections;

public class IronSourceEditorCoroutines
{
    private IEnumerator mRoutine;

    public static IronSourceEditorCoroutines StartEditorCoroutine(IEnumerator routine)
    {
        if (routine == null)
        {
            return null;
        }

        IronSourceEditorCoroutines coroutine = new IronSourceEditorCoroutines(routine);
        coroutine.Start();
        return coroutine;
    }

    IronSourceEditorCoroutines(IEnumerator routine)
    {
        mRoutine = routine;
    }

    void Start()
    {
        EditorApplication.update += Update;
    }

    void Update()
    {
        if (mRoutine != null && mRoutine.MoveNext())
        {
            if (mRoutine.Current is IEnumerator nestedRoutine)
            {
                // If the current yield return value is another IEnumerator,
                // start it as a nested coroutine
                StartEditorCoroutine(nestedRoutine);
            }
        }
        else
        {
            StopEditorCoroutine();
        }
    }

    public void StopEditorCoroutine()
    {
        EditorApplication.update -= Update;
    }
}
