using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dialogo_NPC_7 : FluentScript
{
    int timesVisited = 0;

    public override FluentNode Create()
    {
        return
            Show() *
            Do(() => timesVisited++) *

            If(() => timesVisited == 1,
                Write(0.5f, "Médico:\n...")
            ) *

            If(() => timesVisited == 2,
                Write(0.5f, "Médico:\nNão posso te ajudar agora.") 
            ) *

            If(() => timesVisited > 2,
                Write(0.5f, "Médico:\n(Essas pessoas estão morrendo de gripe? Lá no reino não vemos uma morte por gripe há décadas...)").WaitForButton() *
                Write(0.5f, "Médico:\nPor favor, preciso focar nos doentes.")
            ) *

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
