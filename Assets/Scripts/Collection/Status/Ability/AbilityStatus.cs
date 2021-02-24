using Collection.Status.Player;
using Components.AttributeSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Collection.Status.Ability
{
    public class AbilityStatus : MonoBehaviour
    {
        bool _valorAlterado;

        public Stat Vida { get; private set; }
        public Stat Forca { get; private set; }
        public Stat Agilidade { get; private set; }
        public Stat Velocidade { get; private set; }
        public OnWeaponStatusChangeEvent OnStatusChanged { get; private set; }

        void FixedUpdate()
        {
            if (_valorAlterado) OnStatusChanged.Invoke(this);
        }

        public AbilityStatus Set(PlayerStatusModel atributos)
        {
            Vida = new Stat(atributos.Vida);
            Forca = new Stat(atributos.Forca);
            Agilidade = new Stat(atributos.Agilidade);
            Velocidade = new Stat(atributos.Velocidade);
            OnStatusChanged = new OnWeaponStatusChangeEvent();
            OnStatusChanged.AddListener(status => _valorAlterado = false);
            return this;
        }
    }

    public class OnWeaponStatusChangeEvent : UnityEvent<AbilityStatus> { }
}