using System.Collections;
using Characters;
using Entities;
using UnityEngine;
using Utils.Extension;
using Weapons;

namespace IA
{
    public class DefaultAttackRoutine : MonoBehaviour
    {
        [SerializeField] Entity NPC;
        [SerializeField] Entity target;
        [SerializeField] WeaponData weapon;

        [SerializeField] float distance;

        void Start()
        {
            NPC.EquipaArma(weapon);
        }

        void FixedUpdate()
        {
            if (target == null)
                EncontraInimigo(NPC.transform.position, distance);
            else
                ProcessaInput();

            InimigoDistante();
        }

        void ProcessaInput()
        {
            var targetDistance = target.transform.position - NPC.transform.position;
            NPC.playerMoveParams.lookDiretion = new Vector2(targetDistance.x, targetDistance.z).ToDegree() + 90;
            NPC.playerMoveParams.direction = NPC.playerMoveParams.lookDiretion;
            NPC.playerMoveParams.stoppingDistance = 2;

            if (targetDistance.magnitude < NPC.playerMoveParams.stoppingDistance && !NPC.IsUsingCombo)
            {
                NPC.UsaHabilidade(weapon.Abilities[0]);
                NPC.ParaDeAndar();
                NPC.playerMoveParams.speed = 0;
            }
            else if (targetDistance.magnitude > NPC.playerMoveParams.stoppingDistance)
            {
                NPC.playerMoveParams.speed = 5;
                NPC.MovimentaAte(target.transform.position);
            }
        }

        void EncontraInimigo(Vector3 center, float radius)
        {
            foreach (var collider in Physics.OverlapSphere(center, radius, LayerMask.GetMask("Hittable")))
            {
                if (!collider.attachedRigidbody.transform.TryGetComponent(out Entity otherEntity)) continue;
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