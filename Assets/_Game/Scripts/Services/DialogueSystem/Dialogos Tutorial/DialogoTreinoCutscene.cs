using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DialogoTreinoCutscene : FluentScript
{
    public GameObject timeline;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(4.0f, "Dragg:\nLembre-se, aperte <sprite=\"KeyBoardAndMouse\" name=\"Mouse_Left_Text_Key_Dark\"> para atacar!" +
                       " e <sprite=\"KeyBoardAndMouse\" name=\"Mouse_Right_Text_Key_Dark\"> para usar a técnica especial!").WaitForButton() *
            Write(4.0f, "Dragg:\nQuando precisar, aperte <sprite=\"KeyBoardAndMouse\" name=\"Space_Key_Dark\"> para desviar. " +
                       "Você pode alternar entre seu machado e arco apertando <sprite=\"KeyBoardAndMouse\" name=\"Q_Text_Key_Dark\">").WaitForButton() *
            Write(4.0f, "Dragg:\nTodos esses comandos estão presentes no menu, é só apertar <sprite=\"KeyBoardAndMouse\" name=\"Esc_Text_Key_Dark\">" +
                       " e verificar os controles quando necessário").WaitForButton() *
            Write(4.0f, "Dragg:\nEu também te passei um mapa, você consegue acessá-lo no menu (<sprite=\"KeyBoardAndMouse\" name=\"Esc_Text_Key_Dark\">)." +
                        " Use para saber onde precisa ir quando estiver perdido.").WaitForButton() *
            Write(1.0f, "Dragg:\nChega de papo! Vamo lá, vem com tudo!").WaitForButton() *
            Write(0.5f, "Dragg:\n Não vamos poder treinar por um tempo, estou indo pro reino do sul encontrar um velho conhecido...").WaitForButton() *
            Write(0.5f, "Dragg:\n HYAAA!").WaitForButton() *
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

    public override void OnFinish()
    {
    }

    public override void OnStart()
    {
    }
}