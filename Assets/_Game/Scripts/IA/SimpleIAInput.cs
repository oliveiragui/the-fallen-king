using Entities.Combat;
using Entities.Default;
using UnityEngine;
using Utils.Extension;
using Weapons;

namespace IA
{
    public class SimpleIAInput : MonoBehaviour
    {
        [SerializeField] DefaultEntity NPC;
        [SerializeField] DefaultCommands defaultCommands;

        [SerializeField] CombatEntity NPCCombat;
        [SerializeField] CombatCommands combatCommands;

        [SerializeField] CombatEntity target;

        [SerializeField] WeaponData weaponData;
        Weapon weapon;

        [SerializeField] float distance;

        void Start()
        {
            weapon = new Weapon(weaponData);
            combatCommands.EquipWeapon(weapon);
        }

        void FixedUpdate()
        {
            ProcessaInput();
            // if (target == null)
            //     EncontraInimigo(NPC.transform.position, distance);
            // else
            //
            //     InimigoDistante();
        }

        void ProcessaInput()
        {
            var targetDistance = target.transform.position - NPC.transform.position;
            NPC.data.lookDiretion = new Vector2(targetDistance.x, targetDistance.z).ToDegree() + 90;
            NPC.data.direction = NPC.data.lookDiretion;
            NPC.data.stoppingDistance = distance;

            if (NPCCombat.combatData.UsingCombo)
            {
                combatCommands.StopConjuring(weapon.Abilities[0].Id);
                return;
            }

            if (targetDistance.magnitude < NPC.data.stoppingDistance)
            {
                combatCommands.UseAbility(weapon.Abilities[0]);
                defaultCommands.StopMove();
                NPC.data.speed = 0;
            }
            else if (targetDistance.magnitude > NPC.data.stoppingDistance)
            {
                NPC.data.speed = 5;
                defaultCommands.MoveTo(target.transform.position);
            }
        }

        void EncontraInimigo(Vector3 center, float radius)
        {
            foreach (var collider in Physics.OverlapSphere(center, radius, LayerMask.GetMask("Hittable")))
            {
                if (!collider.attachedRigidbody.transform.TryGetComponent(out CombatEntity otherEntity)) continue;
                if (otherEntity.transform.Equals(NPC.transform)) continue;
                target = otherEntity;
                return;
            }
        }

        void InimigoDistante()
        {
            if (target && (NPC.transform.position - target.transform.position).magnitude > 10) target = null;
        }
    }
}