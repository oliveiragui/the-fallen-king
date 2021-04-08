using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dialogo_NPC_2 : FluentScript
{
    int timesVisited = 0;

    public override FluentNode Create()
    {
        return
            Show() *
            Do(() => timesVisited++) *

            If(() => timesVisited == 1,
                Write(0.5f, "Vendedor:\nDo que você precisa, rapaz? O estoque de hoje já foi.") 
            ) *

            If(() => timesVisited == 2,
                Write(0.5f, "Vendedor:\nO rapaz que traz nossos itens já deve estar fora da vila a essa altura. Volte amanhã.")
            ) *

            If(() => timesVisited > 2,
                Write(0.5f, "Vendedor:\nArgh... Por que tudo tem que ser longe da vila...")
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
