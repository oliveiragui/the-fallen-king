using System;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class Acordar : FluentScript
{
    public GameObject timeline;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(3.0f, "Mulher:\n AAAAAAAHHHHHHHHHHHHHHHH!").WaitForButton() *
            Write(3.0f, "Homem:\n Ninguém sabe onde está o médico, hein?").WaitForButton() *
            Write(3.0f, "Homem:\n Soldados, não deixem ele fugir de novo!").WaitForButton()  *
            Hide() *
            Do(() => IniciaCutscene());
    }

    public override void OnFinish()
    {
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
