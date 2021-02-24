using System.Collections.Generic;
using System.Linq;

namespace ToRefactor.Atributos
{
    public class AtributoComposto : Atributo
    {
        readonly Dictionary<Atributo, float> _dependencias;
        float _base;

        float _valorDependencias;

        public AtributoComposto(float valorBase, float multiplicador = 0) : base(valorBase, multiplicador)
        {
            _base = valorBase;
            _dependencias = new Dictionary<Atributo, float>();
        }

        public new float Valor => base.Valor + _valorDependencias;

        public void AdicionaDependencia(Atributo atributoBonus, float fator)
        {
            _dependencias.Add(atributoBonus, fator);

            CalculaValorBruto();
        }

        public void RemoveDependencia(Atributo atributoBonus)
        {
            if (!_dependencias.ContainsKey(atributoBonus)) return;
            _dependencias.Remove(atributoBonus);
            CalculaValorBruto();
        }

        void CalculaValorBruto()
        {
            _valorDependencias = _dependencias.Sum(dependencia => dependencia.Key.Valor * dependencia.Value);
            CalculaValores();
        }
    }
}