using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dialogo_NPC_4 : FluentScript
{
    int timesVisited = 0;

    public override FluentNode Create()
    {

        return
            Show() *
            Write(0.5f, "Vendedor:\nRapaz, não vou poder te atender, tô fechando a loja agora.").WaitForButton() *
            Write(0.5f, "Vendedor:\nTenho que ir na enfermaria, aquele médico disse que consegue dar um jeito nas minhas costas").WaitForButton() *
            Write(0.5f, "Vendedor:\nHehehe... finalmente vou me livrar dessa maldita dor...") *
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
