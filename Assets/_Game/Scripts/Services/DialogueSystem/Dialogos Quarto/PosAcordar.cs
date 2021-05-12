using System;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class PosAcordar : FluentScript
{
    public GameObject timeline;
    public GameObject TalkPosAcordar;
    [SerializeField] UnityEvent onFinish;
    
    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.25f, "Isso não foi um pesadelo...").WaitForButton() *
            Write(0.25f, "Ainda estou ouvindo gritos.").WaitForButton() *
            Write(0.25f, "Cof... cof... fumaça...").WaitForButton() *
            Write(0.25f, "O que tá acontecendo na vila?!").WaitForButton() *
            Hide();
    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }

    public override void OnStart()
    {
    }

    private void IniciaCutscene()
    {
        if (timeline != null)
        {
            timeline.SetActive(true);

        }
    }
}
