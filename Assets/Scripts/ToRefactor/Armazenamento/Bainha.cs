using Collections.Armas;
using UnityEngine.Events;

//ADICIONAR EVENTO AO TROCAR ARMA

namespace ToRefactor.Armazenamento
{
    public class Bainha
    {
        public readonly Inventario<ArmaController> armazenadas;
        public readonly ArmaController[] equipadas;

        public Bainha(int limiteArmazenamento, int limiteEquipaveis)
        {
            AoMudarArma = new AoMudarArmaEvent();
            equipadas = new ArmaController[limiteEquipaveis];
            armazenadas = new Inventario<ArmaController>(limiteArmazenamento);
        }

        public ArmaController EmUso { get; private set; }
        public AoMudarArmaEvent AoMudarArma { get; }

        public void UsaArma(int index)
        {
            if (index > equipadas.Length) return;
            EmUso = equipadas[index];
            AoMudarArma.Invoke(EmUso);
        }

        public bool AdicionaArma(ArmaController arma)
        {
            return armazenadas.Armazena(arma);
        }

        public bool Equipa(ArmaController arma, int index)
        {
            if (arma == null || arma.EstaEquipado || !armazenadas.Itens.Contains(arma) || index >= equipadas.Length &&
                equipadas[index] != null && !Desequipa(index))
                return false;

            equipadas[index] = arma;
            return arma.EstaEquipado = true;
        }

        public bool Desequipa(int index)
        {
            if (!(equipadas[index] is null) && equipadas[index].Desequipa()) return false;
            equipadas[index] = null;
            return true;
        }
    }

    public class AoMudarArmaEvent : UnityEvent<ArmaController> { }
}