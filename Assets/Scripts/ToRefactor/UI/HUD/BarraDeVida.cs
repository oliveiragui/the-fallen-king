using Collections.Entidades.Utils;
using UnityEngine.UI;

namespace ToRefactor.UI.HUD
{
    public class BarraDeVida
    {
        readonly Slider _barraDeVida;
        readonly StatusEntidade _status;

        public BarraDeVida(Slider barraDeVida, StatusEntidade status)
        {
            _barraDeVida = barraDeVida;
            _status = status;
        }

        public void Atualiza()
        {
            _barraDeVida.value = _status.VidaAtual.Valor / _status.VidaTotal.Valor;
        }
    }
}