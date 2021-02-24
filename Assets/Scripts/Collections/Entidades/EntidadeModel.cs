using Collection.Status.Player;
using Collections.Armas;
using Collections.Equipes;
using UnityEngine;

namespace Collections.Entidades
{
    [CreateAssetMenu(fileName = "Entidade", menuName = "GameContent/Entidade", order = 1)]
    public class EntidadeModel : ScriptableObject
    {
        [field: SerializeField] public PlayerStatusModel AtributosBase { get; private set; }
        [field: SerializeField] public ArmaModel ArmaInicial { get; private set; }
        [field: SerializeField] public EquipeModel Equipe { get; private set; }
    }
}