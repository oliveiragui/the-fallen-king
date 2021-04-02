using System.Collections;
using System.Linq;
using _Game.Scripts.Entities;
using _Game.Scripts.Utils.Extension;
using _Game.Scripts.Weapons;
using UnityEngine;

namespace _Game.Scripts
{
    public class SimpleInput : MonoBehaviour
    {
        [SerializeField] Entity entity;

        [SerializeField] WeaponData[] weapons;

        [SerializeField] EntityCommands entityCommands;

        IEnumerator _weaponCycle;

        void Start()
        {
            foreach (var weapon in weapons) entity.data.associatedCharacter.Weapons.Add(weapon);
            entity.data.associatedCharacter.Weapons.UseWeapon(0);
            entityCommands.EquipWeapon(entity.data.associatedCharacter.Weapons.WeaponInUse);
        }

        void Update()
        {
            ProcessaInput();
        }

        void ProcessaInput()
        {
            var velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var mousePos = FindObjectOfType<Camera>().MouseOnPlane();

            entity.data.direction = velocity.ToDegree() - 225;
            entity.data.lookDiretion = (new Vector2(mousePos.x, mousePos.z) -
                                        new Vector2(entity.transform.position.x, entity.transform.position.z))
                .ToDegree() + 90;
            entity.data.speed = velocity.normalized.sqrMagnitude * 5;
            // //TODO: verificar se está se movendo nos parametros

            if (velocity.magnitude > 0.1)
            {
                entity.animations.Run(entity.data.speed);
            }
            else if (!entityCommands.entity.movement.AutoMovement)
            {
                entityCommands.StopMove();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                entity.data.speed = 1 * 5;
                entityCommands.entity.data.stoppingDistance = 3;
                entityCommands.MoveTo(mousePos);
            }
            var weapon = entity.data.associatedCharacter.Weapons.WeaponInUse;
            
            if (Input.GetButtonDown("Fire1"))
                entityCommands.UseAbility(weapon.Abilities[0]);
            else if (Input.GetButtonDown("Fire2"))
                entityCommands.UseAbility(weapon.Abilities[1]);
            else if (Input.GetButtonDown("Fire3"))
                entityCommands.UseAbility(weapon.Abilities[2]);
            else if (Input.GetButtonDown("Jump"))
                entityCommands.UseAbility(weapon.Abilities[3]);
            if (Input.GetButtonUp("Fire1"))
                entityCommands.StopConjuring(weapon.Abilities[0]);
            if (Input.GetButtonUp("Fire2"))
                entityCommands.StopConjuring(weapon.Abilities[1]);
            if (Input.GetButtonUp("Fire3"))
                entityCommands.StopConjuring(weapon.Abilities[2]);
            if (Input.GetButtonUp("Jump"))
                entityCommands.StopConjuring(weapon.Abilities[3]);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                entity.data.associatedCharacter.Weapons.UseNext();
            }

            //if (Input.GetKeyDown(KeyCode.Q)) abilityCommands.CombatMode(entity.inCombat);
        }
    }
}