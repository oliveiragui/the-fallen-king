using System.Collections;
using _Game.GameModules.Entities.Scripts;
using _Game.GameModules.Weapons.Scripts;
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
            foreach (var weapon in weapons) entity.Character.WeaponStorage.Add(weapon);
            entity.Character.WeaponStorage.UseWeapon(0);
            _camera = FindObjectOfType<Camera>();
        }

        void Update()
        {
            ProcessaInput();
        }

        void OnDisable()
        {
            entity.Stop();
        }

        void ProcessaInput()
        {
            var lookDirection = _camera.MouseOnPlane() - entity.transform.position;
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var direction = Vector3.up * (input.ToDegree() + _camera.transform.rotation.eulerAngles.y);
            var speed = input.normalized.sqrMagnitude * 5;

            entity.LookAt(Quaternion.Euler(Vector3.up * new Vector2(lookDirection.x, lookDirection.z).ToDegree()));

            if (input.sqrMagnitude > 0.1f) entity.Move(speed, Quaternion.Euler(direction));
            else entity.Stop();

            if (Input.GetButtonDown("Fire1")) entity.RequestAbility(0);
            if (Input.GetButtonUp("Fire1")) entity.StopCasting(0);

            if (Input.GetButtonDown("Fire2")) entity.RequestAbility(1);
            if (Input.GetButtonUp("Fire2")) entity.StopCasting(1);

            if (Input.GetButtonDown("Fire3")) entity.RequestAbility(2);
            if (Input.GetButtonUp("Fire3")) entity.StopCasting(2);

            if (Input.GetButtonDown("Jump")) entity.RequestAbility(3);
            if (Input.GetButtonUp("Jump")) entity.StopCasting(3);

            //     if (Input.GetKeyDown(KeyCode.Escape))
            //         entity.associatedCharacter.Weapons.UseNext();
            //     if (Input.GetKeyDown(KeyCode.Q)) entity.movement.AutoMove = !entity.movement.AutoMove;
        }
    }
}