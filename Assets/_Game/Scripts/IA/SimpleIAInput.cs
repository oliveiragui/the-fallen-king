using _Game.Scripts.GameContent.Entities;
using _Game.Scripts.GameContent.Weapons;
using _Game.Scripts.Utils.Extension;
using UnityEngine;

namespace _Game.Scripts.IA
{
    public class SimpleIAInput : MonoBehaviour
    {
        [SerializeField] Entity entity;

        [SerializeField] Entity target;

        [SerializeField] WeaponData weaponData;
        Weapon weapon;

        [SerializeField] float distance;

        void Start()
        {
            // weapon = gameObject.AddComponent<Weapon>().Setup(weaponData);
            // entityCommands.EquipWeapon(weapon);
            entity.associatedCharacter.Weapons.Add(weaponData);
            entity.associatedCharacter.Weapons.UseWeapon(0);
            weapon = entity.associatedCharacter.Weapons.WeaponInUse;
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
            entity.lookDiretion = Quaternion.Euler(Vector3.up * new Vector2(targetDistance.x, targetDistance.z).ToDegree());
            entity.stoppingDistance = distance;

            if (entity.usingAbility)
            {
                entity.StopCasting(0);
            }
            else if (targetDistance.magnitude < entity.stoppingDistance)
            {
                entity.UseAbility(0);
            }
            else if (targetDistance.magnitude > entity.stoppingDistance)
            {
                entity.speed = 5;
                entity.destination = target.transform.position;
                entity.Move();
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