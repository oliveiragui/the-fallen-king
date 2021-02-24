using Components.AttributeSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Collection.Status.Player
{
    public class PlayerStatus : MonoBehaviour
    {
        bool _valorAlterado;

        public Stat Vida { get; private set; }
        public Stat Forca { get; private set; }
        public Stat Agilidade { get; private set; }
        public Stat Velocidade { get; private set; }
        public OnStatusChangeEvent OnStatusChanged { get; private set; }

        void FixedUpdate()
        {
            if (_valorAlterado) OnStatusChanged.Invoke(this);
        }

        public PlayerStatus Set(PlayerStatusModel atributos)
        {
            Vida = new Stat(atributos.Vida);
            Forca = new Stat(atributos.Forca);
            Agilidade = new Stat(atributos.Agilidade);
            Velocidade = new Stat(atributos.Velocidade);
            OnStatusChanged = new OnStatusChangeEvent();
            OnStatusChanged.AddListener(status => _valorAlterado = false);

            return this;
        }
    }

    public class OnStatusChangeEvent : UnityEvent<PlayerStatus> { }
}