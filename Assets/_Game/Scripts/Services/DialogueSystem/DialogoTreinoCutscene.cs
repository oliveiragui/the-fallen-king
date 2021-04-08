using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogoTreinoCutscene : FluentScript
{
    public GameObject timeline;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(1.0f, "Dragg:\n Vamo lá, vem com tudo!.").WaitForButton() *
            Write(0.5f, "Dragg:\n Não vamos poder treinar por um tempo, estou indo pro [REINO_DO_SUL] encontrar um velho conhecido...") *
            Options
            (
                Option("") *
                    Hide() *
                    Do(() => IniciaCutscene()) *
                    End()
            );
    }

    private void IniciaCutscene()
    {
        if (timeline != null)
        {
            timeline.SetActive(true);

        }
    }

    public override void OnFinish()
    {
    }

    public override void OnStart()
    {
    }
}