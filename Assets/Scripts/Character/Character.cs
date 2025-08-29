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
//옵저버를 넣고 뺍니다. 그리고 옵저버가 돈의 흐름을 알리도록 합니다.
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
    private void NotifyMoneyObservers()
    {
        foreach (var observer in _moneyObservers)
        {
            observer.OnMoneyChanged(Money);
        }
    }
    
//아이템을 인벤토리에 저장합니다.
    public void AddItem(ItemData data)
    {
        if (Inventory.Count >= MaxInventoryCount) return;
        //이 때, 인벤토리가 가득찼다면 추가하지 않습니다.
        Inventory.Add(data);
        OnItemAdded?.Invoke();
    }
    
//구매를 시도합니다.
    public bool TryPurchase(int price)
    {
        if (price > Money) return false; //필요한 가격보다 보유한 금액이 적다면 false를,
        //그렇지 않다면 돈을 빼고, 옵저버에게 알린 후, true를 반환합니다.
        Money -= price;
        NotifyMoneyObservers();
        return true;
    }

    private static int GetRequiredExp(int level)
    {
        return level * 3;
    }

    //경험치를 더합니다.
    public void AddExp(int exp)
    {
        Exp += exp;
        while (Exp >= GetRequiredExp(Level))
        {//경험치가 레벨업 요구 경험치보다 많아지면 레벨을 올립니다.
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

        OnLevelUp?.Invoke();//레벨업 시, 인벤토리를 즉시 늘려주기 위함입니다.
    }

    public void Equip(ItemData data)
    {
        switch (data.type)
        {//장비를 장착하고, 추가 공격력/방어력에 값을 넣어줍니다.
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
        {//장비를 해제하고, 추가 공격력/방어력을 없애줍니다.
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
        {//기존의 장비를 해제하고, 새로운 장비를 장착합니다.
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