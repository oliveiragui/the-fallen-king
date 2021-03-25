using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class DialogoPadreMulher : FluentScript
{
    public GameObject timeline;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.5f, "Descanse 3 dias e já estará livre da tosse.").WaitForButton() *
            Write(0.5f, "Obrigado doutor, realmente é um milagre que o senhor esteja na nossa vila!").WaitForButton() *
            Write(0.5f, "Tchau!") *
            Options
            (
                Option("") *
                    Hide() *
                    Do(() => IniciaCutscene()) *
                    End()
            );
                //Hide() *
                //Do(() => IniciaCutscene()) *
                //End();

    }

    public override void OnFinish()
    {
    }

    public override void OnStart()
    {
    }

    private void IniciaCutscene()
    {
        timeline.SetActive(true);

        //if (player != null && playerAnimation != null)
        //{
        //    timeline.get
        //}
    }
}
