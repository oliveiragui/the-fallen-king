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
        Camera _camera;
        IEnumerator _weaponCycle;

        void Start()
        {
            _camera = FindObjectOfType<Camera>();
            foreach (var weapon in weapons) entity.associatedCharacter.Weapons.Add(weapon);
            entity.associatedCharacter.Weapons.UseWeapon(0);
            entity.EquipWeapon(entity.associatedCharacter.Weapons.WeaponInUse);
        }

        void Update()
        {
            ProcessaInput();
        }

        void ProcessaInput()
        {
            
            var lookDirection = _camera.MouseOnPlane() - entity.transform.position;
            entity.lookDiretion =
                Quaternion.Euler(Vector3.up * new Vector2(lookDirection.x, lookDirection.z).ToDegree());
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (direction.sqrMagnitude > 0.1f)
            {
                entity.speed = direction.normalized.sqrMagnitude * 5;
                entity.moveDiretion = Quaternion.Euler(Vector3.up * (direction.ToDegree() + 45));
                entity.Move();
            }
            else entity.Stop();
            
            if (Input.GetButtonDown("Fire1")) entity.UseAbility(0);
            if (Input.GetButtonUp("Fire1")) entity.StopCasting(0);

            if (Input.GetButtonDown("Fire2")) entity.UseAbility(1);
            if (Input.GetButtonUp("Fire2")) entity.StopCasting(1);

            if (Input.GetButtonDown("Fire3")) entity.UseAbility(2);
            if (Input.GetButtonUp("Fire3")) entity.StopCasting(2);

            if (Input.GetButtonDown("Jump")) entity.UseAbility(3);
            if (Input.GetButtonUp("Jump")) entity.StopCasting(3);
      
            
            //     if (Input.GetKeyDown(KeyCode.Escape))
            //         entity.associatedCharacter.Weapons.UseNext();
            //     if (Input.GetKeyDown(KeyCode.Q)) entity.movement.AutoMove = !entity.movement.AutoMove;
        }
    }
}