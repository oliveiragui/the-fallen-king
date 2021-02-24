using Collections.Controles;
using Collections.Habilidades;
using ToRefactor.UI.Menus.Personagem.Abas.Utils;
using UnityEngine;

namespace ToRefactor.UI.Menus.Personagem.Abas
{
    public class AbaHabilidade : MonoBehaviour
    {
        public CartaoHabilidade[] cartoesDeHabilidade;

        public void AtualizaCartao(CartaoHabilidade cartao, string nome, string botao, string descricao)
        {
            cartao.Botao.SetText(botao);
            cartao.Nome.SetText(nome);
            cartao.Descricao.SetText(descricao);
        }

        public void AtualizaCartoes(Controle perfil, HabilidadeController[] habilidades)
        {
            for (int i = 0; i < habilidades.Length; i++)
                Interface.MenuPersonagem.AbaHabilidade.AtualizaCartao(cartoesDeHabilidade[i],
                    habilidades[i].Modelo.Nome, perfil.perfil.PerfilRichText.Esquiva, "Teste");
        }
    }
}