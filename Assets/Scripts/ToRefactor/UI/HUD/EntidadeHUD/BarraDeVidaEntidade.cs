using Collections.Entidades;
using UnityEngine;
using UnityEngine.UI;

namespace ToRefactor.UI.HUD.EntidadeHUD
{
    public class BarraDeVidaEntidade : MonoBehaviour
    {
        [SerializeField] Slider slider;

        [SerializeField] Entidade entidade;

        BarraDeVida barraDeVida;

        void Start()
        {
            barraDeVida = new BarraDeVida(slider, entidade.status);
        }

        void Update()
        {
            barraDeVida?.Atualiza();
        }
    }
}