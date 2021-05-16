﻿using System.Collections;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Entities.Scripts;
using _Game.GameModules.Weapons.Scripts;
using _Game.Scripts.Utils.Extension;
using UnityEngine;

namespace _Game.Scripts
{
    public class SimpleInput : MonoBehaviour
    {
        [SerializeField] Character character;
        [SerializeField] WeaponData[] weapons;
        Camera _camera;
        IEnumerator _weaponCycle;

        void Start()
        {
            foreach (var weapon in weapons) character.WeaponStorage.Add(weapon);
            character.WeaponStorage.UseWeapon(0);
            _camera = FindObjectOfType<Camera>();
        }

        void Update()
        {
            ProcessaInput();
        }

        void OnDisable()
        {
            character.Entity.Stop();
        }

        void ProcessaInput()
        {
            var lookDirection = _camera.MouseOnPlane() - character.transform.position;
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var direction = Vector3.up * (input.ToDegree() + _camera.transform.rotation.eulerAngles.y);
            var speed = input.normalized.sqrMagnitude * 5;

            character.Entity.LookAt(Quaternion.Euler(Vector3.up * new Vector2(lookDirection.x, lookDirection.z).ToDegree()));

            if (input.sqrMagnitude > 0.1f) character.Entity.Move(speed, Quaternion.Euler(direction));
            else character.Entity.Stop();

            if (Input.GetButtonDown("Fire1")) character.AbilitySystem.RequestAbility(0);
            if (Input.GetButtonUp("Fire1")) character.AbilitySystem.StopCasting(0);

            if (Input.GetButtonDown("Fire2")) character.AbilitySystem.RequestAbility(1);
            if (Input.GetButtonUp("Fire2")) character.AbilitySystem.StopCasting(1);

            if (Input.GetButtonDown("Fire3")) character.AbilitySystem.RequestAbility(2);
            if (Input.GetButtonUp("Fire3")) character.AbilitySystem.StopCasting(2);

            if (Input.GetButtonDown("Jump")) character.AbilitySystem.RequestAbility(3);
            if (Input.GetButtonUp("Jump")) character.AbilitySystem.StopCasting(3);

            if (Input.GetKeyUp(KeyCode.Q)) character.WeaponStorage.UseNext();

            //     if (Input.GetKeyDown(KeyCode.Escape))
            //         character.associatedCharacter.Weapons.UseNext();
            //     if (Input.GetKeyDown(KeyCode.Q)) character.movement.AutoMove = !character.movement.AutoMove;
        }
    }
}