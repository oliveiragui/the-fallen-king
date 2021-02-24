using System.Collections.Generic;
using System.Linq;

namespace ToRefactor.Atributos
{
    public class Atributo : AtributoBase
    {
        //UnityEvent m_MyEvent;

        readonly List<AtributoBase> _atributosBonus;

        public Atributo(float valorBase, float multiplicador = 1) : base(valorBase, multiplicador)
        {
            _atributosBonus = new List<AtributoBase>();
            CalculaValores();
        }

        public float Valor { get; private set; }

        float ValorBruto => Base + _atributosBonus.Sum(atributo => atributo.Base);
        float MultiplicadorBruto => 1 + Multiplicador + _atributosBonus.Sum(atributo => atributo.Multiplicador);

        public void AdicionaBonus(AtributoBase atributoBonus)
        {
            _atributosBonus.Add(atributoBonus);
            CalculaValores();
        }

        public void RemoveBonus(AtributoBase atributoBonus)
        {
            if (!_atributosBonus.Contains(atributoBonus)) return;
            _atributosBonus.Remove(atributoBonus);

            CalculaValores();
        }

        protected void CalculaValores()
        {
            Valor = ValorBruto * Multiplicador;
            //AddAoMudarValorListener(()=> Debug.Log("oi"));
        }

        // public void AddAoMudarValorListener(UnityAction valor)
        // {
        //     m_MyEvent.AddListener(valor);
        // }
    }
}