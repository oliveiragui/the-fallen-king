using _Game.GameModules.Abilities.Scripts;
using _Game.Scripts.Services.AttributeSystem;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    [CreateAssetMenu(fileName = "Fire Projectile", menuName = "GameContent/Entities/Commands/Fire Projectile", order = 0)]
    public class FireProjectileCommand : EntityCommand
    {
        [SerializeField] AttributeModifier power;
        [SerializeField] float angle;

        public override void Execute(Entity entity)
        {
            power.Calculate(entity.Character.CharacterStatus.Strength);
            var tr = entity.transform;
            var position = tr.position + Vector3.up;
            var hit = new AbilityHit(power.Value, Vector3.zero, entity.Character);
            var arrow = entity.Character.WeaponStorage.WeaponInUse.ammoData.Instantiate(position,
                Quaternion.Euler(Vector3.up * (tr.rotation.eulerAngles.y + angle)));
            arrow.Shot(hit, arrow.transform.forward * 800);
        }
    }
}