using _Game.Scripts.Entities;
using _Game.Scripts.Utils.Extension;
using _Game.Scripts.Weapons;
using UnityEngine;

namespace _Game.Scripts.IA
{
    public class SimpleIAInput : MonoBehaviour
    {
        [SerializeField] Entity entity;
        [SerializeField] EntityCommands entityCommands;
        

        [SerializeField] Entity target;

        [SerializeField] WeaponData weaponData;
        Weapon weapon;

        [SerializeField] float distance;

        void Start()
        {
            // weapon = gameObject.AddComponent<Weapon>().Setup(weaponData);
            // entityCommands.EquipWeapon(weapon);
            entity.data.associatedCharacter.Weapons.Add(weaponData);
            entity.data.associatedCharacter.Weapons.UseWeapon(0);
            weapon = entity.data.associatedCharacter.Weapons.WeaponInUse;
        }

        void FixedUpdate()
        {
            ProcessaInput();
            // if (target == null)
            //     EncontraInimigo(entity.transform.position, distance);
            // else
            //
            //     InimigoDistante();
        }

        void ProcessaInput()
        {
            var targetDistance = target.transform.position - entity.transform.position;
            entity.data.lookDiretion = new Vector2(targetDistance.x, targetDistance.z).ToDegree() + 90;
            entity.data.direction = entity.data.lookDiretion;
            entity.data.stoppingDistance = distance;

            if (entity.data.UsingCombo)
            {
                entityCommands.StopConjuring(weapon.Abilities[0]);
                return;
            }

            if (targetDistance.magnitude < entity.data.stoppingDistance)
            {
                entityCommands.UseAbility(weapon.Abilities[0]);
                entityCommands.StopMove();
                entity.data.speed = 0;
            }
            else if (targetDistance.magnitude > entity.data.stoppingDistance)
            {
                entity.data.speed = 5;
                entityCommands.MoveTo(target.transform.position);
            }
        }

        void EncontraInimigo(Vector3 center, float radius)
        {
            foreach (var collider in Physics.OverlapSphere(center, radius, LayerMask.GetMask("Hittable")))
            {
                if (!collider.attachedRigidbody.transform.TryGetComponent(out Entity otherEntity)) continue;
                if (otherEntity.transform.Equals(entity.transform)) continue;
                target = otherEntity;
                return;
            }
        }

        void InimigoDistante()
        {
            if (target && (entity.transform.position - target.transform.position).magnitude > 10) target = null;
        }
    }
}