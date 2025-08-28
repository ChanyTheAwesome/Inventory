using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public int Level { get; private set; }
    public int Exp { get; private set; }
    public float AttackDmg { get; private set; }
    public float Armor { get; private set; }
    public float Health { get; private set; }
    public float CriticalRate { get; private set; }
    public int Money { get; private set; }

    private float _additionalAttackDmg;
    private float _additionalArmor;
    public float FinalAttackDmg => AttackDmg + _additionalAttackDmg;
    public float FinalArmor => Armor + _additionalArmor;
    public event Action OnLevelUp;
    [HideInInspector] public ItemData EquippedWeapon;
    [HideInInspector] public ItemData EquippedArmor;
    public List<ItemData> Inventory = new List<ItemData>();
    public Character(int level, int exp, float attackDmg, float armor, float health, float criticalRate, int money)
    {
        Level = level;
        Exp = exp;
        AttackDmg = attackDmg;
        Armor = armor;
        Health = health;
        CriticalRate = criticalRate;
        Money = money;
    }
    public void LevelUp()
    {
        Level++;
        Exp -= Level * 3;
        AttackDmg += 5;
        Armor += 3;
        Health += 100;
        
        OnLevelUp?.Invoke();
    }

    public void Equip(ItemData data)
    {
        switch (data.Type)
        {
            case ItemType.Weapon:
                EquippedWeapon = data;
                EquippedWeapon.IsEquipped = true;
                _additionalAttackDmg = data.Value;
                break;
            case ItemType.Armor:
                EquippedArmor = data;
                EquippedArmor.IsEquipped = true;
                _additionalArmor = data.Value;
                break;
        }
    }

    public void Disarm(ItemData data)
    {
        switch (data.Type)
        {
            case ItemType.Weapon:
                data.IsEquipped = false;
                EquippedWeapon = null;
                _additionalAttackDmg = 0;
                break;
            case ItemType.Armor:
                data.IsEquipped = false;
                EquippedArmor = null;
                _additionalArmor = 0;
                break;
        }
    }

    public void EquipNew(ItemData data)
    {
        switch (data.Type)
        {
            case ItemType.Weapon:
                Disarm(EquippedWeapon);
                Equip(data);
                break;
            case ItemType.Armor:
                Disarm(EquippedArmor);
                Equip(data);
                break;
        }
    }
}
