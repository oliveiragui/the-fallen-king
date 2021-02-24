using Collections.Avatares;
using UnityEngine;

namespace ToRefactor.UI.HUD.EntidadeHUD
{
    public class AvatarHUD : MonoBehaviour
    {
        public AvatarController avatar;
        public CaixaDeDialogoEmJogo caixaDeDialogoEmJogo;
        public GameObject gameObjectBarraDeVida;
        public BarraDeVidaEntidade barraDeVida;

        void FixedUpdate()
        {
            transform.position = avatar.transform.position;
        }
    }
}