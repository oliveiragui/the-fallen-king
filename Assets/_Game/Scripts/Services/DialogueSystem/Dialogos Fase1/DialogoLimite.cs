using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoLimite : FluentScript
{
    public GameObject timeline;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return Do(() => AtivaGameObject(timeline));
    }
    
    private void AtivaGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);

        }
    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }

    public override void OnStart()
    {
        character.StopWalking();
        onStart.Invoke();
    }

    private void IniciaCutscene()
    {
        if (timeline != null)
        {
            timeline.SetActive(true);
        }
    }
}
