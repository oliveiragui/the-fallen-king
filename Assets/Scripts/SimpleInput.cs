using System.Collections;
using Entities;
using UnityEngine;
using Utils.Extension;
using Weapons;

public class SimpleInput : MonoBehaviour
{
    [SerializeField] Entity entity;
    [SerializeField] WeaponData weapon;
    [SerializeField] WeaponData[] weapons;

    IEnumerator _weaponCycle;

    void Start()
    {
        weapon = weapons[0];
        _weaponCycle = SwitchWeapon();
    }

    void Update()
    {
        ProcessaInput();
    }

    IEnumerator SwitchWeapon()
    {
        while (true)
            foreach (var nextWeapon in weapons)
            {
                entity.EquipaArma(nextWeapon);
                weapon = nextWeapon;
                yield return null;
            }
    }

    void ProcessaInput()
    {
        var velocity = new Vector2(Input.GetAxisRaw("P1KHorizontal"), Input.GetAxisRaw("P1KVertical"));
        var mousePos = FindObjectOfType<Camera>().MouseOnPlane();

        entity.playerMoveParams.direction = velocity.ToDegree() - 225;
        entity.playerMoveParams.lookDiretion = new Vector2(mousePos.x, mousePos.z).ToDegree();
        entity.playerMoveParams.speed = velocity.normalized.sqrMagnitude * 5;

        // if (entity.movement.IsMoving && !entity.movement.AutoMovement)
        //     entity.ParaDeAndar();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            entity.playerMoveParams.stoppingDistance = 3;
            entity.MovimentaAte(mousePos);
        }

        if (Input.GetButtonDown("P1KAtaque1")) entity.UsaHabilidade(weapon.Abilities[0]);
        else if (Input.GetButtonDown("P1KAtaque2")) entity.UsaHabilidade(weapon.Abilities[1]);
        else if (Input.GetButtonDown("P1KAtaque3")) entity.UsaHabilidade(weapon.Abilities[2]);
        else if (Input.GetButtonDown("P1KEsquiva")) entity.UsaHabilidade(weapon.Abilities[3]);

        if (Input.GetButtonUp("P1KAtaque1")) entity.ParaDeConjurar(weapon.Abilities[0].Id);
        if (Input.GetButtonUp("P1KAtaque2")) entity.ParaDeConjurar(weapon.Abilities[1].Id);
        if (Input.GetButtonUp("P1KAtaque3")) entity.ParaDeConjurar(weapon.Abilities[2].Id);
        if (Input.GetButtonUp("P1KEsquiva")) entity.ParaDeConjurar(weapon.Abilities[3].Id);

        if (Input.GetKeyDown(KeyCode.Escape)) _weaponCycle.MoveNext();
        if (Input.GetKeyDown(KeyCode.Q)) entity.InCombat = !entity.InCombat;
    }
}