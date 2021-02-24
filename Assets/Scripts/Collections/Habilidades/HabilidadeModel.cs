using System;
using Collections.Acoes;
using Collections.Avatares;
using UnityEngine;

namespace Collections.Habilidades
{
    [CreateAssetMenu(fileName = "Habilidade", menuName = "GameContent/Habilidade", order = 2)]
    public class HabilidadeModel : ScriptableObject
    {
        [field: SerializeField] public string Nome { get; private set; }
        [field: SerializeField] public string Descricao { get; private set; }
        [field: SerializeField] public string SpriteText { get; private set; }
        [field: SerializeField] public StatusHabilidade Status { get; private set; }
        [field: SerializeField] public bool Bloqueavel { get; private set; }
        [field: SerializeField] public bool Bloqueante { get; private set; }
        [field: SerializeField] public string NomeDaAcao { get; private set; }
        [field: SerializeField] public int AnimationID { get; private set; }

        //[field: SerializeField] public AbilityAction test { get; private set; }

        public bool PodeInterromper(HabilidadeModel outraHabilidadeModel)
        {
            return outraHabilidadeModel == null || Bloqueante && outraHabilidadeModel.Bloqueavel;
        }

        public AbilityAction InstanciaAcao(AvatarController avatar, HabilidadeController habilidade)
        {
            // var test2 = Instantiate(test.gameObject, avatar.transform);
            // var test3 = test2.GetComponent<AbilityAction>();
            // test3.Avatar = avatar;
            // test3.HabilidadeController = habilidade;
            // test3.Inicializa();
            var actionType = Type.GetType($"Collections.Acoes.Habilidades.{NomeDaAcao}");
            var abilityAction = avatar.Actions.AddComponent(actionType) as AbilityAction;
            if (abilityAction == null) throw new ArgumentNullException("Ação não encontrada");

            abilityAction.Habilidade = habilidade;
            abilityAction.Inicializa(avatar);

            return abilityAction;
        }
    }

    [Serializable]
    public class StatusHabilidade
    {
        [field: SerializeField] public int ComboMaximo { get; private set; }
        [field: SerializeField] public float Alcance { get; private set; }
        [field: SerializeField] public float Duracao { get; private set; }
        [field: SerializeField] public float TempoDeRecarga { get; private set; }
    }
}