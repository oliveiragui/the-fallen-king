using _Game.GameModules.Abilities.Scripts;
using _Game.Scripts.Services.AttributeSystem;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Hit Area", menuName = "GameContent/Entities/Commands/Hit Area", order = 0)]
    public class HitAreaCommand : EntityCommand
    {
        [SerializeField] AttributeModifier power;
        [SerializeField] HitImpact impact;
        [SerializeField] HitType type;
        [SerializeField] float radius;
        [SerializeField] Vector3 positionOffset;

        public override void Execute(Entity entity)
        {
            power.Calculate(entity.Character.CharacterStatus.Strength);

            var tr = entity.transform;
            var hits = Physics.SphereCastAll(
                tr.position + positionOffset,
                radius,
                tr.forward,
                0.01f,
                LayerMask.GetMask("Hittable"));

            foreach (var hit in hits)
            {
                if (!hit.collider.attachedRigidbody.transform.TryGetComponent(out Entity otherEntity)) continue;
                if (!otherEntity.Hittable) continue;
                if (otherEntity.Character.Equals(entity.Character)) continue;
                if (otherEntity.Character.Team.PlayerFriend ==
                    entity.Character.Team.PlayerFriend) continue;

                otherEntity.Hit(new AbilityHit(power.Value, Vector3.zero, entity.Character, impact, type));
            }
        }
    }
}