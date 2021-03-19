using System.Collections;
using System.Linq;
using Entities.Combat;
using Entities.Default;
using UnityEngine;
using Utils.Extension;
using Weapons;

public class SimpleInput : MonoBehaviour
{
    [SerializeField] CombatEntity entity;

    [SerializeField] Weapon weapon;
    [SerializeField] Weapon[] weaponsx;
    [SerializeField] WeaponData[] weapons;

    [SerializeField] CombatCommands combatCommands;
    [SerializeField] DefaultCommands defaultCommands;

    IEnumerator _weaponCycle;

    void Start()
    {
        weaponsx = weapons.Select(weapon => gameObject.AddComponent<Weapon>().Setup(weapon)).ToArray();
        _weaponCycle = SwitchWeapon();
    }

    void Update()
    {
        ProcessaInput();
    }

    IEnumerator SwitchWeapon()
    {
        while (true)
            foreach (var nextWeapon in weaponsx)
            {
                combatCommands.EquipWeapon(nextWeapon);
                weapon = nextWeapon;
                yield return null;
            }
    }

    void ProcessaInput()
    {
        var velocity = new Vector2(Input.GetAxisRaw("P1KHorizontal"), Input.GetAxisRaw("P1KVertical"));
        var mousePos = FindObjectOfType<Camera>().MouseOnPlane();

        entity.defaultData.direction = velocity.ToDegree() - 225;
        entity.defaultData.lookDiretion = (new Vector2(mousePos.x, mousePos.z) -
                                           new Vector2(entity.transform.position.x, entity.transform.position.z))
            .ToDegree() + 90;
        entity.defaultData.speed = velocity.normalized.sqrMagnitude * 5;
        // //TODO: verificar se está se movendo nos parametros

        if (velocity.magnitude > 0.1)
        {
            entity.defaultData.animations.Run(entity.defaultData.speed);
        }
        else if (!defaultCommands.entity.components.movement.AutoMovement)
        {
            defaultCommands.StopMove();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            entity.defaultData.speed = 1 * 5;
            defaultCommands.entity.data.stoppingDistance = 3;
            defaultCommands.MoveTo(mousePos);
        }

        //if (weapon.Abilities == null) return;

        if (Input.GetButtonDown("P1KAtaque1")) combatCommands.UseAbility(weapon.Abilities[0]);
        else if (Input.GetButtonDown("P1KAtaque2")) combatCommands.UseAbility(weapon.Abilities[1]);
        else if (Input.GetButtonDown("P1KAtaque3")) combatCommands.UseAbility(weapon.Abilities[2]);
        else if (Input.GetButtonDown("P1KEsquiva")) combatCommands.UseAbility(weapon.Abilities[3]);
        if (Input.GetButtonUp("P1KAtaque1")) combatCommands.StopConjuring(weapon.Abilities[0].Id);
        if (Input.GetButtonUp("P1KAtaque2")) combatCommands.StopConjuring(weapon.Abilities[1].Id);
        if (Input.GetButtonUp("P1KAtaque3")) combatCommands.StopConjuring(weapon.Abilities[2].Id);
        if (Input.GetButtonUp("P1KEsquiva")) combatCommands.StopConjuring(weapon.Abilities[3].Id);

        if (Input.GetKeyDown(KeyCode.Escape)) _weaponCycle.MoveNext();
        //if (Input.GetKeyDown(KeyCode.Q)) abilityCommands.CombatMode(entity.inCombat);
    }
}