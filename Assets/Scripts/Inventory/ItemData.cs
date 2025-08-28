using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    //Misc,
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData
{
    public string Name;
    public string Description;
    public ItemType Type;
    public float Value;
    public Sprite Icon;

    public bool IsEquippable;
    public bool IsEquipped;
    //public bool isStackable;
    //public int stackSize;
    //public GameObject prefab;
}
