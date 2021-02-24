using System;
using Collection.Status.Player;
using Collections.Avatares.Componentes;
using Collections.Habilidades;
using UnityEngine;

namespace Collections.Armas
{
    [CreateAssetMenu(fileName = "Arma", menuName = "GameContent/Arma", order = 1)]
    public class ArmaModel : ScriptableObject
    {
        //[field: SerializeField] public TipoDeArma TipoDeArma { get; private set; }
        [field: SerializeField] public HabilidadeModel Ataque1 { get; private set; }
        [field: SerializeField] public HabilidadeModel Ataque2 { get; private set; }
        [field: SerializeField] public HabilidadeModel Ataque3 { get; private set; }
        [field: SerializeField] public HabilidadeModel Esquiva { get; private set; }
        [field: SerializeField] public bool Descartavel { get; private set; }
        [field: SerializeField] public string Nome { get; private set; }
        [field: SerializeField] public int AnimationID { get; private set; }
        [field: SerializeField] public PlayerStatusModel AtributosBase { get; private set; }

        [field: SerializeField] public PrefabDeArma[] Prefabs { get; private set; }
    }

    [Serializable]
    public class PrefabDeArma
    {
        [field: SerializeField] public SlotMesh Slot { get; private set; }
        [field: SerializeField] public GameObject GameObject { get; private set; }
    }
}