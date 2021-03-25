using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fluent;

public class MulherVaiEmbora : FluentScript
{
    public GameObject timeline;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0.5f, "Tchau!") *
            Options
            (
                Option("") *
                    Hide() *
                    Do(() => IniciaCutscene()) *
                    End()
            );
        //Hide() *
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
