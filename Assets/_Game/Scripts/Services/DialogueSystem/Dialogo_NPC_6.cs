using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dialogo_NPC_6 : FluentScript
{
    int timesVisited = 0;

    public override FluentNode Create()
    {

        return
            Show() *
            Write(0.5f, "Mulher:\nOh meu bem, nós encontramos alguém que poderia ter te curado.").WaitForButton() *
            Write(0.5f, "Mulher:\nAh, se ao menos você visse como estamos melhores...").WaitForButton() *
            Write(0.5f, "Vendedor:\nEu tenho tanta saudades...") *
            Options
            (
                Option("") *
                    Hide() *
                    End()
            );

    }
    // void OnTriggerExit(Collider collider)
    // {
    // }

    public override void OnFinish()
    {
    }

    public override void OnStart()
    {
    }
}
