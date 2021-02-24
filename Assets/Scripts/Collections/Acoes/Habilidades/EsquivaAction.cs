using Collections.Avatares;
using Collections.Avatares.Componentes;
using UnityEngine;

namespace Collections.Acoes.Habilidades
{
    public class EsquivaAction : AbilityAction
    {
        CharacterController charController;

        public override void Inicializa(AvatarController avatar)
        {
            base.Inicializa(avatar);
            charController = Avatar.GetComponent<CharacterController>();
        }

        public override void OnStateEntered()
        {
            base.OnStateEntered();
            Avatar.Parametros.EmMovimento = false;
            Avatar.Parametros.Atingivel = false;
            Avatar.Audio.TocaSom(SlotSom.Esquiva);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            charController.SimpleMove(Avatar.transform.forward *
                                      (Avatar.entidade.status.VelocMovimento.Valor * 1.7f * Time.fixedDeltaTime));
        }

        public override void OnStateExited()
        {
            base.OnStateExited();
            Avatar.Parametros.Atingivel = true;
        }
    }
}