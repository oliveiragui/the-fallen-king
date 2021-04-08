using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dialogo_NPC_3 : FluentScript
{
    int timesVisited = 0;

    public override FluentNode Create()
    {
        return
            Show() *
            Do(() => timesVisited++) *

            If(() => timesVisited == 1,
                Write(0.5f, "Vendedor:\nJovem, não tenho mais nada que possa te vender, volte amanhã.")
            ) *

            If(() => timesVisited == 2,
                Write(0.5f, "Vendedor:\nAh, não liga para aquele ranzinza ali na loja da frente. É o meu irmão.").WaitForButton() * 
                Write(0.5f, "Vendedor:\nEle odeia ter que parar de trabalhar por causa do estoque...").WaitForButton() *
                Write(0.5f, "Vendedor:\nMal sabe ele o quão tranquila é nossa vida aqui na vila, nunca fomos roubados nem nada...")
            ) *

            If(() => timesVisited > 2,
                Write(0.5f, "Vendedor:\nÉ, essa tranquilidade eu não troco por nada...")
            ) *

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
