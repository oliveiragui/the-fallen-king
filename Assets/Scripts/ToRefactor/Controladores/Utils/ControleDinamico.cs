using System;
using System.Collections.Generic;
using System.Linq;
using Collections.Controles;
using Collections.Controles.Utils;
using ToRefactor.Controladores.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace ToRefactor.Controladores.Utils
{
    public class ControleDinamico : MonoBehaviour
    {
        public readonly AoTrocarControleEvent aoTrocarControle = new AoTrocarControleEvent();
        public Dictionary<TipoDeControle, Controle> _controles;
        public Controle ativo { get; private set; }

        void FixedUpdate()
        {
            if (_controles != null) VerificaTrocaDeControle();
        }

        public void Configura(JogadorID jogadorID)
        {
            _controles = new Dictionary<TipoDeControle, Controle>();
            _controles.Add(TipoDeControle.MouseETeclado, new Controle(Controles.Instance.Modelos[0], jogadorID));
            _controles.Add(TipoDeControle.Gamepad, new Controle(Controles.Instance.Modelos[1], jogadorID));
            ativo = _controles[TipoDeControle.MouseETeclado];
        }

        public virtual void AoTrocarControle(Controle perfil) => aoTrocarControle.Invoke(perfil);

        void VerificaTrocaDeControle()
        {
            var inputRealizado = VerificaInputRealizado();
            if (inputRealizado == ativo.perfil.Tipo) return;
            ativo = _controles[inputRealizado];
            AoTrocarControle(ativo);
        }

        TipoDeControle VerificaInputRealizado()
        {
            foreach (var controle in _controles.Values.Where(controle => controle.ControleUsado))
                return controle.perfil.Tipo;
            return ativo.perfil.Tipo;
        }
    }

    [Serializable]
    public class AoTrocarControleEvent : UnityEvent<Controle> { }
}