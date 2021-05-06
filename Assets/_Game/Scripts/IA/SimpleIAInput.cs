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
            entity.Character.WeaponStorage.Add(weaponData);
            entity.Character.WeaponStorage.UseWeapon(0);
            entity.AutoMove = true;
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
            entity.LookAt(Quaternion.Euler(Vector3.up * new Vector2(targetDistance.x, targetDistance.z).ToDegree()));
            if (entity.Character.AbilitySystem.AbilityInUse)
                entity.StopCasting(0);
            else if (targetDistance.magnitude <= distance)
            {
                entity.Stop();
                entity.RequestAbility(0);
            }
            else if (targetDistance.magnitude > distance)
                entity.MoveTo(5, target.transform.position, distance);
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
    }
}