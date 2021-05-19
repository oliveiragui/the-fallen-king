﻿using System;
using _Game.GameModules.Entities.Scripts;
using _Game.GameModules.Weapons.Scripts;
using _Game.Scripts.Utils.Extension;
using UnityEngine;

namespace _Game.GameModules.IA.Scripts
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
            entity.Character.WeaponStorage.Add(weaponData);
            entity.Character.WeaponStorage.UseWeapon(0);
            entity.AutoMove = true;

            minDistance = preferedDistance - maxVariation / 2;
            maxDistance = preferedDistance + maxVariation / 2;
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
            animator.SetBool("Possui Alvo", target);

            if (target)
            {
                var targetDistance = entity.transform.position - target.transform.position;
                AnalizaDistancia(targetDistance);
            }
        }

        public void LookToTarget()
        {
            var position = entity.transform.position;
            var targetPosition = Target.transform.position;
            var direction = targetPosition - position;
            entity.Direction = Quaternion.Euler(Vector3.up * new Vector2(direction.x, direction.z).ToDegree());
            entity.LookDiretion = (Quaternion.Euler(Vector3.up * new Vector2(direction.x, direction.z).ToDegree()));
            entity.Destination = Target.transform.position;
        }

        void AnalizaDistancia(Vector3 distance)
        {
            if (target)
            {
                animator.SetBool("Muito Distante", distance.magnitude > maxDistance);
                animator.SetBool("Muito Proximo", distance.magnitude < minDistance);
                animator.SetBool("Alvo Atras", TargetIsBehind());

                for (var i = 0; i < entity.Character.AbilitySystem.Abilities.Count; i++)
                {
                    animator.SetBool($"Cooldown Habilidade {i + 1}",
                        entity.Character.AbilitySystem.Abilities[i].OnCooldown);
                }
            }

            //if (target && (entity.transform.position - target.transform.position).magnitude > 10) target = null;
        }

        bool TargetIsBehind()
        {
            var transform1 = entity.transform;
            var toTarget = (target.transform.position - transform1.position).normalized;
            return Vector3.Dot(toTarget, transform1.forward) < 0;
        }
    }
}