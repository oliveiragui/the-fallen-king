using System;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoSaiuDaVila : FluentScript
{
    public GameObject timeline;

    public override FluentNode Create()
    {
        return
            Show() *
            Write("PLAYER_PRINCIPAL:\nE aí, está pronto?").WaitForButton() *
            Write("Pirin:\nImpossível dizer, com tudo que nos aguarda no futuro...").WaitForButton() *
            Write("PLAYER_PRINCIPAL:\nEu estava falando das suas coisas. Conseguiu recolher seus pertences?").WaitForButton() *
            Write("Pirin:\nAh. Minhas coisas. Sim. Estou pronto... E você?").WaitForButton() *
            Write("PLAYER_PRINCIPAL:\nEu tenho minhas armas, e agora um médico me acompanhando... Eu acho que não posso esperar por muito mais que isso.").WaitForButton() *
            Write("Pirin:\nPeço perdão por te envolver numa guerra que não é sua.").WaitForButton() *
            Write("PLAYER_PRINCIPAL:\nSou um mercenário, eu ganho dinheiro lutando as batalhas dos outros. Não se preocupe.").WaitForButton() *
            Write("Pirin:\nÉ verdade... Bom, vamos indo, teremos que passar pela floresta ainda.").WaitForButton() *
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