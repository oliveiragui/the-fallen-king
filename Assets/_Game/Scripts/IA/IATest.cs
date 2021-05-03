using System;
using _Game.Scripts.GameContent.Entities;
using _Game.Scripts.GameContent.Weapons;
using UnityEngine;

namespace _Game.Scripts.IA
{
    public class IATest : MonoBehaviour
    {
        public Entity entity;
        [SerializeField] Entity target;

        [SerializeField] Animator animator;
        [SerializeField] WeaponData weaponData;

        [NonSerialized] public float minDistance;
        [NonSerialized] public float maxDistance;
        
        public float preferedDistance;
        [Range(1,10)]
        public float maxVariation;

        public Entity Target => target;

        void Start()
        {
            entity.Character.WeaponStorage.Add(weaponData);
            entity.Character.WeaponStorage.UseWeapon(0);
            entity.AutoMove = true;

            minDistance = preferedDistance - maxVariation;
            maxDistance = preferedDistance + maxVariation;
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

        void AnalizaDistancia(Vector3 distance)
        {
            if (target)
            {
                animator.SetBool("Muito Distante", distance.sqrMagnitude > maxDistance);
                animator.SetBool("Muito Proximo", distance.sqrMagnitude < minDistance);
            }

            //if (target && (entity.transform.position - target.transform.position).magnitude > 10) target = null;
        }
    }
}