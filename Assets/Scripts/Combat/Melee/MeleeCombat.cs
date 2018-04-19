﻿using UnityEngine;

/// <summary>
/// Made by Koen Sparreboom
/// </summary>
public class MeleeCombat : EquipListener {
    [SerializeField]
    private MeleeType _meleeType;

    [SerializeField]
    private DamageTrigger _damageTrigger;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private string _animatorAttackState = "Attack";

    private void Start() {
        _damageTrigger.Damage = _meleeType.Damage;
    }

    private void Update() {
        /*if (Input.GetButtonDown("Fire1")) {
            Attack();
        }*/

        _damageTrigger.Enabled = _animator.GetCurrentAnimatorStateInfo(0).IsName(_animatorAttackState);
    }

    public void ChangeWeapon(MeleeType meleeType) {
        _meleeType = meleeType;
        _damageTrigger.Damage = _meleeType.Damage;
    }

    protected override void OnItemEquip(ItemObject itemObject) {
        if (itemObject.Type == ItemObject.ItemType.Weapon) {
            ChangeWeapon(itemObject.MeleeType);
        }
    }
}