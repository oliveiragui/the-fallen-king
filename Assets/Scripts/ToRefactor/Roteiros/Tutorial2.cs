using System.Collections;
using Collections.Controles.Utils;
using ToRefactor.Controladores.Enums;
using ToRefactor.Controladores.Utils;
using ToRefactor.Gerenciadores;
using ToRefactor.IA;
using ToRefactor.Interagiveis;
using ToRefactor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToRefactor.Roteiros
{
    public class Tutorial2 : MonoBehaviour
    {
        [SerializeField] AreaInteragivel AreaDeBatalha;
        [SerializeField] CaixaDeDialogo Dialogo;

        [SerializeField] NPCcomIA medico;
        [SerializeField] NPCcomIA primeiroGuerreiro;

        [SerializeField] NPCcomIA npcDoBando1;
        [SerializeField] NPCcomIA npcDoBando2;
        [SerializeField] NPCcomIA npcDoBando3;
        ControleDinamico _controle;

        Jogador _jogador;

        void Start()
        {
            StartCoroutine(ScriptTutorial());
        }

        IEnumerator ScriptTutorial()
        {
            yield return new WaitUntil(() => GerenciadorDeJogo.Instance.Jogadores[JogadorID.P1] != null);

            _jogador = GerenciadorDeJogo.Instance.Jogadores[JogadorID.P1];

            yield return new WaitUntil(() => _jogador.Controlador.Controle != null);

            _controle = _jogador.Controlador.Controle;
            primeiroGuerreiro.entidade.Avatar.entidade.Equipe.aliado = true;

            yield return EncontraAreaDeTreino();
            yield return EsperaUsuarioAceitarConvite();
            yield return EsperaBatalharComPrimeiroInimigo();
            yield return AmigosDoGuerreiroOAjudam();
            yield return EsperaMedicoAlcancarJogador();
            //

            SceneManager.LoadScene(0);
        }

        IEnumerator EsperaBatalharComPrimeiroInimigo()
        {
            iniciaAtaque(primeiroGuerreiro);

            yield return MostraDialogoPorAlgunsSegundos("Elimine seu inimigo", 3, Dialogo);

            yield return new WaitUntil(() => primeiroGuerreiro.entidade.status.VidaAtual.Valor <= 0);

            primeiroGuerreiro.target = null;
            primeiroGuerreiro.npcQueAtaca.PodeAtacar = false;
        }

        IEnumerator AmigosDoGuerreiroOAjudam()
        {
            iniciaAtaque(npcDoBando1);
            iniciaAtaque(npcDoBando2);
            iniciaAtaque(npcDoBando3);

            yield return MostraDialogoPorAlgunsSegundos("Oh não, você derrotou Eidor! Pode com ele mas não com a gente",
                3,
                npcDoBando1.avatarHUD.caixaDeDialogoEmJogo);

            yield return new WaitUntil(() =>
                npcDoBando1.entidade.status.VidaAtual.Valor <= 0 &&
                npcDoBando2.entidade.status.VidaAtual.Valor <= 0 &&
                npcDoBando3.entidade.status.VidaAtual.Valor <= 0);

            npcDoBando1.target = null;
            npcDoBando1.npcQueAtaca.PodeAtacar = false;
            npcDoBando2.target = null;
            npcDoBando2.npcQueAtaca.PodeAtacar = false;
            npcDoBando3.target = null;
            npcDoBando3.npcQueAtaca.PodeAtacar = false;
        }

        IEnumerator MostraDialogoPorAlgunsSegundos(string mensagem, int tempo, CaixaDeDialogo dialogo)
        {
            primeiroGuerreiro.avatarHUD.gameObjectBarraDeVida.SetActive(true);
            dialogo.Mostra(mensagem);
            yield return new WaitForSeconds(tempo);
            dialogo.CaixaDeTexto.SetActive(false);
        }

        void iniciaAtaque(NPCcomIA npc)
        {
            npc.avatarHUD.gameObjectBarraDeVida.SetActive(true);
            npc.entidade.Avatar.entidade.Equipe.aliado = false;
            npc.target = _jogador.entidade.Avatar.transform;
            npc.npcQueAtaca.PodeAtacar = true;
        }

        IEnumerator EsperaUsuarioAceitarConvite()
        {
            var comando = primeiroGuerreiro.entidade.Avatar.Comando;
            var dialogo = primeiroGuerreiro.avatarHUD.caixaDeDialogoEmJogo;

            yield return new WaitUntil(() =>
            {
                var position = _jogador.entidade.Avatar.transform.position;
                comando.MovimentaAtePonto(position);
                return (position - primeiroGuerreiro.entidade.Avatar.transform.position).magnitude < 5;
            });
            comando.Idle();

            yield return new WaitUntil(() =>
            {
                dialogo.Mostra("Olá! Então você é o famoso mercenário? te desafio para uma treino");
                Dialogo.Mostra($"Aperte {_controle.ativo.perfil.PerfilRichText.Confirma} Aceitar o Desafio");
                return _controle.ativo.GetButtonDown(Botao.Confirma);
            });
            dialogo.CaixaDeTexto.SetActive(false);
            Dialogo.CaixaDeTexto.SetActive(false);
        }

        IEnumerator EsperaMedicoAlcancarJogador()
        {
            var comando = medico.entidade.Avatar.Comando;
            var dialogo = medico.avatarHUD.caixaDeDialogoEmJogo;

            yield return new WaitUntil(() =>
            {
                var position = _jogador.entidade.Avatar.transform.position;
                comando.MovimentaAtePonto(position);
                return (position - medico.entidade.Avatar.transform.position).magnitude < 5;
            });
            comando.Idle();

            yield return new WaitUntil(() =>
            {
                dialogo.Mostra("Olá! Grande show! Assisti sua luta, magnifico!");
                Dialogo.Mostra($"Aperte {_controle.ativo.perfil.PerfilRichText.Confirma} para continuar");
                return _controle.ativo.GetButtonDown(Botao.Confirma);
            });

            yield return null;

            yield return new WaitUntil(() =>
            {
                dialogo.Mostra(
                    "Soube que você é um mercenário. Aceitaria uns trocados para me ajudar a cruzar a floresta do reino");
                Dialogo.Mostra($"Aperte {_controle.ativo.perfil.PerfilRichText.Confirma} Aceitar a Missão");
                return _controle.ativo.GetButtonDown(Botao.Confirma);
            });

            Dialogo.CaixaDeTexto.SetActive(false);
        }

        IEnumerator EncontraAreaDeTreino()
        {
            Dialogo.Texto.text = "Encontre a área de treinamento";
            Dialogo.CaixaDeTexto.SetActive(true);
            yield return new WaitForSeconds(7);
            Dialogo.CaixaDeTexto.SetActive(false);
            yield return new WaitUntil(() => AreaDeBatalha.jogadorDentro);
        }
    }
}