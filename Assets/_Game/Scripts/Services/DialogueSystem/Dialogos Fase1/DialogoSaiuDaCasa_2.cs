using System;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoSaiuDaCasa_2 : FluentScript
{
    [SerializeField] UnityEvent onFinish;
    public GameObject timeline;

    public override FluentNode Create()
    {
        return
            Show() *
            Write("Soldado:\nEntão essa é sua resposta...").WaitForButton() *
            Write("Soldado:\nVocê vai se arrepender, rapaz!").WaitForButton() *
            Do(() => IniciaCutscene()) *
            Hide();

    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }
    private void IniciaCutscene()
    {
        if (timeline != null)
        {
            timeline.SetActive(true);

        }
    }
}