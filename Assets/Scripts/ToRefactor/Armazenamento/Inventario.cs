using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace ToRefactor.Armazenamento
{
    public class Inventario<T>
    {
        readonly List<T> _itens;

        public Inventario(int capacidade)
        {
            _itens = new List<T>();
            AoAlterarInventario = new AoAlterarInventarioEvent<T>();
            Capacidade = capacidade;
        }

        public AoAlterarInventarioEvent<T> AoAlterarInventario { get; }

        public List<T> Itens => new List<T>(_itens);

        public int Capacidade
        {
            get => _itens.Capacity;
            set
            {
                if (value < _itens.Count)
                    throw new ArgumentOutOfRangeException("Novo limite menor do que o número de itens armazenados");
                _itens.Capacity = value;
            }
        }

        public bool Armazena(T item)
        {
            if (_itens.Capacity <= _itens.Count || item == null) return false;
            _itens.Add(item);
            AoAlterarInventario.Invoke(Itens);
            return true;
        }

        public virtual bool Remove(T item)
        {
            if (!_itens.Remove(item)) return false;
            AoAlterarInventario.Invoke(Itens);
            return true;
        }
    }

    public class AoAlterarInventarioEvent<T> : UnityEvent<List<T>> { }
}