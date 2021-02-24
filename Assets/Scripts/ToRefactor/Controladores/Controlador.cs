using Collections.Controles.Utils;
using Collections.Entidades;
using ToRefactor.Controladores.Enums;
using ToRefactor.Controladores.Utils;
using ToRefactor.UI;
using UnityEngine;

namespace ToRefactor.Controladores
{
    public class Controlador : MonoBehaviour
    {
        public ControleDinamico Controle { get; private set; }
        [field: SerializeField] public InputAvatar InputAvatar { get; private set; }
        [field: SerializeField] public InputInterface InputInterface { get; private set; }
        [field: SerializeField] public Entidade Entidade { get; private set; }

        void Start()
        {
            if (InputAvatar == null) InputAvatar = gameObject.AddComponent<InputAvatar>();
            if (InputInterface == null) InputInterface = gameObject.AddComponent<InputInterface>();
            if (Controle == null) Controle = gameObject.AddComponent<ControleDinamico>();

            Controle.Configura(JogadorID.P1);
            InputAvatar.Configura(Controle, Entidade.Avatar);
            InputInterface.Configura(this);

            Controle.aoTrocarControle.AddListener(perfil =>
            {
                bool tipoDeControle = perfil.perfil.Tipo == TipoDeControle.Gamepad;
                Interface.EventSystemGamepad.gameObject.SetActive(tipoDeControle);
                Interface.EventSystemTeclado.gameObject.SetActive(!tipoDeControle);
            });
        }

        public void Configura(Entidade entidade)
        {
            Entidade = entidade;
        }

        public void InputAvatarHabilidado(bool valor)
        {
            InputAvatar.enabled = valor;
        }
    }
}