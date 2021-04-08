using System.Collections;
using _Game.Scripts.GameContent.Entities;
using _Game.Scripts.GameContent.Weapons;
using _Game.Scripts.Utils.Extension;
using UnityEngine;

namespace _Game.Scripts
{
    public class SimpleInput : MonoBehaviour
    {
        [SerializeField] Entity entity;
        [SerializeField] WeaponData[] weapons;
        [SerializeField] EntityCommands entityCommands;
        Camera _camera;
        IEnumerator _weaponCycle;

        void Start()
        {
            foreach (var weapon in weapons) entity.associatedCharacter.Weapons.Add(weapon);
            entity.associatedCharacter.Weapons.UseWeapon(0);
            entityCommands.EquipWeapon(entity.associatedCharacter.Weapons.WeaponInUse);
            _camera = FindObjectOfType<Camera>();
        }

        void Update()
        {
            ProcessaInput();
        }

        void ProcessaInput()
        {
            var weapon = entity.associatedCharacter.Weapons.WeaponInUse;
            var mousePos = _camera.MouseOnPlane();
            var lookDirection = mousePos - entity.transform.position;
            entity.lookDiretion =
                Quaternion.Euler(Vector3.up * new Vector2(lookDirection.x, lookDirection.z).ToDegree());

            if (entity.movement.AutoMove)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    entity.movement.Speed = 1* 5;
                    entity.movement.StoppingDistance = 3;
                    entity.movement.Destination = mousePos;
                }
            }

            else
            {
                var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                if (direction.sqrMagnitude > 0.1f)
                {
                    entity.movement.Speed = direction.normalized.sqrMagnitude * 5;
                    entity.movement.Rotation = Quaternion.Euler(Vector3.up * (direction.ToDegree() + 45));
                }
                   
                else entity.commands.Stop();
            }

            if (Input.GetButtonDown("Fire1"))
                entityCommands.UseAbility(weapon.Abilities[0]);
            else if (Input.GetButtonDown("Fire2"))
                entityCommands.UseAbility(weapon.Abilities[1]);
            else if (Input.GetButtonDown("Fire3"))
                entityCommands.UseAbility(weapon.Abilities[2]);
            else if (Input.GetButtonDown("Jump"))
                entityCommands.UseAbility(weapon.Abilities[3]);
            if (Input.GetButtonUp("Fire1"))
                entityCommands.StopCasting(weapon.Abilities[0]);
            if (Input.GetButtonUp("Fire2"))
                entityCommands.StopCasting(weapon.Abilities[1]);
            if (Input.GetButtonUp("Fire3"))
                entityCommands.StopCasting(weapon.Abilities[2]);
            if (Input.GetButtonUp("Jump"))
                entityCommands.StopCasting(weapon.Abilities[3]);
            if (Input.GetKeyDown(KeyCode.Escape))
                entity.associatedCharacter.Weapons.UseNext();
            if (Input.GetKeyDown(KeyCode.Q)) entity.movement.AutoMove = !entity.movement.AutoMove;
        }
    }
}