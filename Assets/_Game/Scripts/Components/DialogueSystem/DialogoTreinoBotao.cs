using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogoTreinoBotao : FluentScript
{
    int timesVisited = 0;

    public override FluentNode Create()
    {
        return
            Show() *
            Do(() => timesVisited++) *

            If(() => timesVisited == 1,
                Write(0.5f, "Dragg:\nVolte a falar comigo quando estiver pronto pro treino!")
            ) *

            If(() => timesVisited >= 2,
                Write(0.5f, "Dragg:\nPra atacar aperte <sprite=\"KeyBoardAndMouse\" name=\"Mouse_Left_Key_Dark\"> ").WaitForButton() *
                Write(0.5f, "Dragg:\nPra esquivar aperte <sprite=\"KeyBoardAndMouse\" name=\"Space_Key_Dark\"> ").WaitForButton() *
                Write(0.5f, "Dragg:\nPra trocar de arma aperte <sprite=\"KeyBoardAndMouse\" name=\"Tab_Key_Dark\"> ")
            ) *

            //If(() => timesVisited > 2,
            //    Write(0.5f, "Vendedor:\nArgh... Por que tudo tem que ser longe da vila...")
            //) *

            Options
            (
                Option("") *
                    Hide() *
                    End()
             );


    }

    public override void OnFinish()
    {
    }

    public override void OnStart()
    {
    }
}