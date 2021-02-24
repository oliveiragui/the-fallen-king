using System.Collections.Generic;
using Collections.Entidades;
using ToRefactor.UI.Menus.Personagem.Abas.Utils;
using UnityEngine;

namespace ToRefactor.UI.Menus.Personagem.Abas
{
    public class AbaInventario : MonoBehaviour
    {
        public List<SlotInventario> armasEquipadas;
        public List<SlotInventario> armasAdquiridas;
        public List<SlotInventario> consumiveis;
        public List<SlotInventario> itens;

        public void Atualiza(Entidade entidade)
        {
            var equipadas = entidade.Bainha.equipadas;

            for (int i = 0; i < equipadas.Length; i++)
            {
                itens[i].Button.onSubmit.RemoveAllListeners();
                if (equipadas[i] != null)
                {
                    int i1 = i;
                    itens[i].Button.onSubmit.AddListener(test =>
                    {
                        entidade.Bainha.UsaArma(i1);
                        Sons.SomDeTrocarDeArma.Play();
                    });

                    itens[i].Text.text = equipadas[i].Modelo.Nome;
                }
            }
        }
    }
}