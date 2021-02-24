using Collections.Entidades;
using UnityEngine;
using UnityEngine.UI;

namespace ToRefactor.UI.HUD.PainelPerfil
{
    public class PainelStatusJogador : MonoBehaviour
    {
        [SerializeField] Slider VidaSlider;
        BarraDeVida barraDeVida;
        Entidade entidade;

        void Start()
        {
            barraDeVida = new BarraDeVida(VidaSlider, entidade.status);
        }

        void Update()
        {
            barraDeVida?.Atualiza();
        }
    }
}