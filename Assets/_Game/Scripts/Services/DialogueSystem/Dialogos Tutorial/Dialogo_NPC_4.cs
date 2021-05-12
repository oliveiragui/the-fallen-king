using Fluent;
using System.Collections;
using System.Collections.Generic;
using _Game.GameModules.Entities.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Dialogo_NPC_4 : FluentScript
{
    int timesVisited = 0;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {

        return
            Show() *
            Write(0.5f, "Vendedor:\nRapaz, não vou poder te atender, tô fechando a loja agora.").WaitForButton() *
            Write(0.5f, "Vendedor:\nTenho que ir na enfermaria, aquele médico disse que consegue dar um jeito nas minhas costas").WaitForButton() *
            Write(0.5f, "Vendedor:\nHehehe... finalmente vou me livrar dessa maldita dor...").WaitForButton() *
            Hide();

    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }

    public override void OnStart()
    {
        character.Stop();
        onStart.Invoke();
    }
}
