using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public int Level { get; private set; }
    public int Exp { get; private set; }
    private float _attackDmg;
    private float _armor;
    public float Health { get; private set; }
    public float CriticalRate { get; private set; }
    public int Money { get; private set; }

    private float _additionalAttackDmg;
    private float _additionalArmor;
    public float FinalAttackDmg => _attackDmg + _additionalAttackDmg;
    public float FinalArmor => _armor + _additionalArmor;
    public event Action OnLevelUp;
    public event Action OnItemAdded;
    [HideInInspector] public ItemData EquippedWeapon;
    [HideInInspector] public ItemData EquippedArmor;
    public readonly List<ItemData> Inventory = new List<ItemData>();
    private readonly List<IMoneyObserver> _moneyObservers = new List<IMoneyObserver>();
    public int MaxInventoryCount { get; private set; }

    public Character(int level, int exp, float attackDmg, float armor, float health, float criticalRate, int money)
    {
        Level = level;
        Exp = exp;
        _attackDmg = attackDmg;
        _armor = armor;
        Health = health;
        CriticalRate = criticalRate;
        Money = money;
        MaxInventoryCount = 20;
    }

    public void AddMoneyObserver(IMoneyObserver observer)
    {
        if (!_moneyObservers.Contains(observer))
            _moneyObservers.Add(observer);
    }

    public void RemoveObserver(IMoneyObserver observer)
    {
        if (_moneyObservers.Contains(observer))
            _moneyObservers.Remove(observer);
    }

    public void AddItem(ItemData data)
    {
        if (Inventory.Count >= MaxInventoryCount) return;
        Inventory.Add(data);
        OnItemAdded?.Invoke();
    }

    private void NotifyMoneyObservers()
    {
        foreach (var observer in _moneyObservers)
        {
            observer.OnMoneyChanged(Money);
        }
    }

    public bool TryPurchase(int price)
    {
        if (price > Money) return false;

        Money -= price;
        NotifyMoneyObservers();
        return true;
    }

    private static int GetRequiredExp(int level)
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

    private void LevelUp()
    {
        Level++;
        _attackDmg += 5;
        _armor += 3;
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
            default:
                Debug.LogError("Wrong item type");
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
            default:
                Debug.LogError("Wrong item type");
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
            default:
                Debug.LogError("Wrong item type");
                break;
        }
    }
}