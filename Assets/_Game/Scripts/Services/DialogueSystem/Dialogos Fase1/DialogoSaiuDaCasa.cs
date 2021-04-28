using System;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoSaiuDaCasa : FluentScript
{
    public GameObject timeline;

    public override FluentNode Create()
    {
        return
        Show() *
        Write("Soldado:\nOlha só o que temos aqui...").WaitForButton() *
        Write("Soldado:\nQuem diria, um mercenário numa vilazinha dessa").WaitForButton() *
        Write("Soldado:\nEscuta só, rapaz. Se você não resistir e falar pra gente onde tá o médico").WaitForButton() *
        Write("Soldado:\n...posso falar pro general te contratar pro nosso exército. O que acha?").WaitForButton() *
        Do(() => IniciaCutscene()) *
        Hide();

    }

    private void IniciaCutscene()
    {
        if (timeline != null)
        {
            timeline.SetActive(true);

        }
    }
}