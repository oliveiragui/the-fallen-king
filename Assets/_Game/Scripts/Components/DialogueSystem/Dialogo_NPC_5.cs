using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dialogo_NPC_5 : FluentScript
{
    int timesVisited = 0;

    public override FluentNode Create()
    {

        return
            Show() *
            Write(0.5f, "Padre:\nMinhas preces foram atendidas... Ninguém mais precisa morrer doente...").WaitForButton() *
            Write(0.5f, "Padre:\nMeu Deus, finalmente há cura na nossa vila...").WaitForButton() *
            Write(0.5f, "Vendedor:\nÉ um milagre... esse médico estar aqui... só pode ser um milagre...") *
            Options
            (
                Option("") *
                    Hide() *
                    End()
            );

    }
    void OnTriggerExit(Collider collider)
    {
    }

    public override void OnFinish()
    {
    }

    public override void OnStart()
    {
    }
}
