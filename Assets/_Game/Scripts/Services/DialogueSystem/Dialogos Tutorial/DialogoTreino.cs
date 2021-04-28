using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogoTreino : FluentScript
{
    int timesVisited = 0;

    public override FluentNode Create()
    {
        return
            Do(() => timesVisited++) *

            If(() => timesVisited == 1,
                Yell("I havent seen you before") *
                Yell("Lets be friends!") *
                Yell("Talk to me again sometime")
            ) *

            If(() => timesVisited == 2,
                Yell("I'm going to count your visits!")
            ) *

            If(() => timesVisited >= 2,
                Yell("Visit number " + timesVisited));

    }

    public override void OnFinish()
    {
    }

    public override void OnStart()
    {
    }
}