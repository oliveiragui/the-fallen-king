using System;
using _Game.GameModules.Entities.Scripts;
using _Game.GameModules.InputControllers.IA.Scripts.AttacksInRangeIA.Behaviours;
using _Game.GameModules.Weapons.Scripts;
using _Game.Scripts.Utils.Extension;
using UnityEngine;

namespace _Game.GameModules.IA.Scripts.AttacksInRangeIA
{
    public class IATest : MonoBehaviour
    {
        public Entity entity;
        [SerializeField] Entity target;

        [SerializeField] Animator animator;
        [SerializeField] WeaponData weaponData;

        public float preferedDistance;
        [Range(1, 10)] public float maxVariation;
        [NonSerialized] public float maxDistance;
        [NonSerialized] public float minDistance;

        public Entity Target => target;

        void Start()
        {
            foreach (var behaviour in animator.GetBehaviours<IaBehaviour>()) behaviour._test = this;
            entity.Character.WeaponStorage.Add(weaponData);
            entity.Character.WeaponStorage.UseWeapon(0);
            entity.AutoMove = true;
            minDistance = preferedDistance - maxVariation / 2;
            maxDistance = preferedDistance + maxVariation / 2;
        }

        void Update()
        {
            ProcessaInput();
        }

        void ProcessaInput()
        {
            animator.SetBool("Possui Alvo", target);
            // entity.InputSpeed = 1;
            if (target)
            {
                var targetDistance = entity.transform.position - target.transform.position;
                AnalizaDistancia(targetDistance);
                AnalizaHabilidades();
                LookTo(target.transform.position);
            }
        }

        void AnalizaDistancia(Vector3 distance)
        {
            animator.SetBool("Muito Distante", distance.magnitude > maxDistance);
            animator.SetBool("Muito Proximo", distance.magnitude < minDistance);
            animator.SetBool("Alvo Atras", TargetIsBehind());
        }

        bool TargetIsBehind()
        {
            var transform1 = entity.transform;
            var toTarget = (target.transform.position - transform1.position).normalized;
            return Vector3.Dot(toTarget, transform1.forward) < 0;
        }

        void AnalizaHabilidades()
        {
            for (var i = 0; i < entity.Character.AbilitySystem.Abilities.Count; i++)
            {
                animator.SetBool($"Cooldown Habilidade {i + 1}",
                    entity.Character.AbilitySystem.Abilities[i].OnCooldown);
            }
        }

        public void MoveTo(Vector3 target)
        {
            var direction = target - entity.transform.position;
            entity.Direction = Quaternion.Euler(Vector3.up * new Vector2(direction.x, direction.z).ToDegree());
            entity.Destination = target;
        }

        public void LookTo(Vector3 target)
        {
            var direction = target - entity.transform.position;
            entity.LookDiretion = (Quaternion.Euler(Vector3.up * new Vector2(direction.x, direction.z).ToDegree()));
        }
    }
}