using System.Collections.Generic;
using Collections.Entidades;
using UnityEngine;
using UnityEngine.UI;

namespace ToRefactor.UI.HUD
{
    public class GerenciadorInvetario : MonoBehaviour
    {
        [SerializeField] List<Button> buttons;
        Entidade entidade;

        // Update is called once per frame
        void Update()
        {
            var itensGuardados = entidade.Bainha.armazenadas;

            // foreach (var item in itensGuardados)
            // {
            //     var index = itensGuardados.IndexOf(item);
            //
            //     buttons[index].onClick.RemoveAllListeners();
            //     buttons[index].onClick.AddListener(() =>
            //     {
            //         //entidade.equipamentos.EquipaItem(item as EquipamentoBase);
            //     });
            //
            //     var text = buttons[index].gameObject.GetComponentInChildren<Text>();
            //     text.text = item.Nome;
            // }
        }
    }
}