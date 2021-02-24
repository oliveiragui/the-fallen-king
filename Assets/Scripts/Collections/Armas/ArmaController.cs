using UnityEngine;

namespace Collections.Armas
{
    public class ArmaController
    {
        public ArmaController(ArmaModel modelo)
        {
            Modelo = modelo;
        }

        [field: SerializeField] public bool EstaEquipado { get; set; }
        public ArmaModel Modelo { get; }

        public ArmaParams Params { get; }

        public bool Equipa()
        {
            return Params.EstaEquipado = true;
        }

        public bool Desequipa()
        {
            return Params.EstaEquipado = false;
        }
    }
}