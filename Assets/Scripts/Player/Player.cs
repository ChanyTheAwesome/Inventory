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
    public event Action OnItemAdded;
    [HideInInspector] public ItemData EquippedWeapon;
    [HideInInspector] public ItemData EquippedArmor;
    public List<ItemData> Inventory = new List<ItemData>();
    public int MaxInventoryCount { get; private set; }

    public Character(int level, int exp, float attackDmg, float armor, float health, float criticalRate, int money)
    {
        Level = level;
        Exp = exp;
        AttackDmg = attackDmg;
        Armor = armor;
        Health = health;
        CriticalRate = criticalRate;
        Money = money;
        MaxInventoryCount = 20;
    }

    public void AddItem(ItemData data)
    {
        if (Inventory.Count < MaxInventoryCount)
        {
            Inventory.Add(data);
            OnItemAdded?.Invoke();
        }
    }
    private int GetRequiredExp(int level)
    {
        return level * 3;
    }
    public void AddExp(int exp)
    {
        Exp += exp;
        while (Exp >= GetRequiredExp(Level))
        {
            Exp -= GetRequiredExp(Level);
            LevelUp();
        }
    }
    public void LevelUp()
    {
        Level++;
        AttackDmg += 5;
        Armor += 3;
        Health += 100;
        MaxInventoryCount += 5;
        
        OnLevelUp?.Invoke();
    }

    public void Equip(ItemData data)
    {
        switch (data.type)
        {
            case ItemType.Weapon:
                EquippedWeapon = data;
                _additionalAttackDmg = data.value;
                break;
            case ItemType.Armor:
                EquippedArmor = data;
                _additionalArmor = data.value;
                break;
        }
    }

    public void Disarm(ItemData data)
    {
        switch (data.type)
        {
            case ItemType.Weapon:
                EquippedWeapon = null;
                _additionalAttackDmg = 0;
                break;
            case ItemType.Armor:
                EquippedArmor = null;
                _additionalArmor = 0;
                break;
        }
    }

    public void EquipNew(ItemData data)
    {
        switch (data.type)
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
