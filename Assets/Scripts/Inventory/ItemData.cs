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
public class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public ItemType type;
    public float value;
    public Sprite icon;

    public bool isEquippable;
    //public bool isStackable;
    //public int stackSize;
    //public GameObject prefab;
}
