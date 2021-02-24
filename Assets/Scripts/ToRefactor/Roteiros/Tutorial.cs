using System.Collections;
using Collections.Controles.Utils;
using ToRefactor.Controladores.Enums;
using ToRefactor.Controladores.Utils;
using ToRefactor.Gerenciadores;
using ToRefactor.Interagiveis;
using ToRefactor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToRefactor.Roteiros
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] CaixaDeDialogo Dialogo;

        [SerializeField] AreaInteragivel areaDePegarArma;
        [SerializeField] CaixaDeDialogoEmJogo DialogoPegaArma;

        [SerializeField] AreaInteragivel areaSairDoJogo;
        [SerializeField] CaixaDeDialogoEmJogo DialogoSaiDoQuarto;
        ControleDinamico _controle;
        InputAvatar _ctrlAvatar;

        Jogador _jogador;

        void Start()
        {
            StartCoroutine(ScriptTutorial());
        }

        IEnumerator ScriptTutorial()
        {
            Sons.SomDeSino.Play();

            yield return new WaitUntil(() => GerenciadorDeJogo.Instance.Jogadores[JogadorID.P1] != null);
            _jogador = GerenciadorDeJogo.Instance.Jogadores[JogadorID.P1];

            yield return new WaitUntil(() => _jogador.Controlador.InputAvatar != null);

            _ctrlAvatar = _jogador.Controlador.InputAvatar;
            _controle = _jogador.Controlador.Controle;

            _ctrlAvatar.podeUsarHabilidade = false;
            _ctrlAvatar.podeMirar = true;

            yield return EsperaJogadorPegarArmas();

            yield return EsperaAbrirEFecharInventario();

            yield return EsperaJogadorSair();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        IEnumerator EsperaJogadorPegarArmas()
        {
            yield return new WaitUntil(() =>
            {
                if (!areaDePegarArma.jogadorDentro)
                {
                    Dialogo.Texto.SetText("Va ate a mesa e \n pegue suas armas");
                    Dialogo.CaixaDeTexto.SetActive(true);
                }
                else
                {
                    DialogoPegaArma.Texto.SetText(
                        $"Pressione {_controle.ativo.perfil.PerfilRichText.Confirma} para pegar suas armas");
                    Dialogo.CaixaDeTexto.SetActive(false);
                }

                return areaDePegarArma.interagiu;
            });

            areaDePegarArma.transform.gameObject.SetActive(false);
        }

        IEnumerator EsperaAbrirEFecharInventario()
        {
            Dialogo.CaixaDeTexto.SetActive(true);

            yield return new WaitUntil(() =>
            {
                Dialogo.Texto.SetText(
                    $"Pressione {_controle.ativo.perfil.PerfilRichText.MenuDeJogo} para ver seu Inventário");
                return _controle.ativo.GetButtonDown(Botao.MenuDeJogo);
            });

            Dialogo.CaixaDeTexto.SetActive(false);
            yield return null;

            yield return new WaitWhile(() => Interface.MenuPersonagem.MenuAberto);

            _ctrlAvatar.podeUsarHabilidade = true;
        }

        IEnumerator EsperaJogadorSair()
        {
            yield return new WaitUntil(() =>
            {
                if (!areaSairDoJogo.jogadorDentro)
                {
                    Dialogo.Texto.SetText("Saia do seu quarto");
                    Dialogo.CaixaDeTexto.SetActive(true);
                }
                else
                {
                    DialogoSaiDoQuarto.Texto.SetText(
                        $"Pressione {_controle.ativo.perfil.PerfilRichText.Confirma} para sair do quarto");
                    Dialogo.CaixaDeTexto.SetActive(false);
                }

                return areaSairDoJogo.interagiu;
            });
        }
    }
}