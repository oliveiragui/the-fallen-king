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
        var speed = new Vector2(Input.GetAxisRaw("P1KHorizontal"), Input.GetAxisRaw("P1KVertical"));
        if (speed.magnitude > 0.1)
            entity.Movimenta(5, speed.ToDegree()-225);
        else
            entity.ParaDeAndar();

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