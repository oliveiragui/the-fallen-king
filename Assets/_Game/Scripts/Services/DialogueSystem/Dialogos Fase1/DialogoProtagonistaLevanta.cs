using System;
using _Game.GameModules.Entities.Scripts;
using Fluent;
using UnityEngine;
using UnityEngine.Events;

public class DialogoProtagonistaLevanta : FluentScript
{
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;
    public GameObject timeline;
    [SerializeField] Entity character;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onFinish;
    [SerializeField] UnityEvent dialogueTrigger;

    public override FluentNode Create()
    {
        return
            Show() *
            Write("Sieg:\nAcho que já estou melhor. Então, o que você estava dizendo...").WaitForButton() *
            Write("Pirin:\nAcho que não tem problema te contar... Embora eles tenham me dito para não confiar em ninguém de fora dos Legisladores...").WaitForButton() *
            Write("Pirin:\nVocê ligou bem os pontos. Eu não sou apenas um dos Legisladores, como também sou um dos co-fundadores. " +
                            "Minha mãe é a mesma enfermeira por quem o Rei se apaixonou. Eu sou Pirin, herdeiro do trono do Reino do Norte.").WaitForButton() *
            Write("Pirin:\nMuito prazer, jovem mercenário. Poderia me dizer seu nome?").WaitForButton() *
            Write("Sieg:\nMeu nome é Sieg.").WaitForButton() *
            Write("Pirin:\nEu tenho outra pergunta pra te fazer. O exército já sabe que eu estou nessa vila, aparentemente, logo eles devem checar o que acontenceu.").WaitForButton() *
            Write("Pirin:\nPreciso chegar ao Reino do Sul, temos alguns aliados a nossa causa que podem nos ajudar na batalha.").WaitForButton() *
            Write("Pirin:\nVocê levantaria sua espada pela causa dos Legisladores? O pagamento é garantido, claro.").WaitForButton() *
            Write("Sieg:\nEu aceito o trabalho, também tenho motivos pra ir ao Reino do Sul. O Dragg tinha viajado pra lá não faz muito tempo").WaitForButton() *
            Write("Pirin:\nÉ reconfortante saber que posso contar com a ajuda do homem que derrotou o General Labahn.").WaitForButton() *
            Write("Pirin:\nBom, assim que estiver pronto, me espere na saída da vila. Vou terminar de tratar os pacientes e recolher meus pertences e te encontro lá").WaitForButton() *
            Hide();
    }


    private void AtivaGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
        }
    }

    public override void OnFinish()
    {
        onFinish.Invoke();
    }

    private void DialogueTrigger()
    {
        dialogueTrigger.Invoke();
    }
}