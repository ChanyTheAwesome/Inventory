using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private UISlot _uiSlot;
    public int index;
    [HideInInspector] public ItemData itemData;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI equippedText;

    private void Start()
    {
        button.onClick.AddListener(OnClickSlot);
    }

    public void Init(UISlot ui, int indexNumber)
    {
        _uiSlot = ui;
        index = indexNumber;
        if (GameManager.Instance.Character.Inventory.Count > index)
        {
            SetItem(GameManager.Instance.Character.Inventory[index]);
        }
        else
        {
            equippedText.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
        }
    }

    private void SetItem(ItemData data)
    {
        itemData = data;
        equippedText.gameObject.SetActive(itemData.IsEquipped);
        button.gameObject.SetActive(true);
    }

    private void OnClickSlot()
    {
        if (itemData.IsEquippable)
        {
            if (itemData.IsEquipped)
            {
                GameManager.Instance.Character.Disarm(itemData);
            }
            else
            {
                switch (itemData.Type)
                {
                    case ItemType.Weapon:
                        if (GameManager.Instance.Character.EquippedWeapon != null)
                        {
                            GameManager.Instance.Character.EquipNew(itemData);
                        }
                        else
                        {
                            GameManager.Instance.Character.Equip(itemData);
                        }
                        break;
                    case ItemType.Armor:
                        if (GameManager.Instance.Character.EquippedArmor != null)
                        {
                            GameManager.Instance.Character.EquipNew(itemData);
                        }
                        else
                        {
                            GameManager.Instance.Character.Equip(itemData);
                        }
                        break;
                }
            }
        }
    }
}