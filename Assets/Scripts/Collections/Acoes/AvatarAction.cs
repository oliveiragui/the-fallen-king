using Collections.Avatares;
using Components.StateMachinePattern;

namespace Collections.Acoes
{
    public class AvatarAction : State
    {
        public AvatarController Avatar { get; set; }

        public virtual void Inicializa(AvatarController avatar)
        {
            Avatar = avatar;
        }
    }
}