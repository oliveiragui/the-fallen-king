using System;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoPosBoss : FluentScript
{
    public GameObject timeline;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
             Yell(3.0f, "Sieg:\nCof... Cof... A fumaça tá me deixando meio tonto...") *
             Do(() => AtivaGameObject(timeline));
    }

    private void AtivaGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);

        }
    }

    public override void OnStart()
    {
        onStart.Invoke();
    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }
}