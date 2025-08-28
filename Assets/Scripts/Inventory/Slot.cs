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
    [SerializeField] private Image image;
    public TextMeshProUGUI equippedText;
    [HideInInspector] public int currentEquippedWeaponIndex;
    [HideInInspector] public int currentEquippedArmorIndex;

    private void Start()
    {
        button.onClick.AddListener(OnClickSlot);
    }
    
    public void Init(UISlot ui, int indexNumber)
    {
        _uiSlot = ui;
        index = indexNumber;
        SetUI();
    }

    public void SetUI()
    {
        if (GameManager.Instance.Character.Inventory.Count > index)
        {
            itemData = GameManager.Instance.Character.Inventory[index];
            image.sprite = itemData.icon;
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
        }

        if (_uiSlot == null)
        {
            return;
        }
        if(_uiSlot.equippedWeaponIndex == index || _uiSlot.equippedArmorIndex == index)
        {
            equippedText.gameObject.SetActive(true);
        }
        else
        {
            equippedText.gameObject.SetActive(false);
        }
    }

    private void OnClickSlot()
    {
        if (itemData.isEquippable)
        {
            if (_uiSlot.equippedArmorIndex == index)
            {
                _uiSlot.equippedArmorIndex = -1;
                GameManager.Instance.Character.Disarm(itemData);
            }
            else if (_uiSlot.equippedWeaponIndex == index)
            {
                _uiSlot.equippedWeaponIndex = -1;
                GameManager.Instance.Character.Disarm(itemData);
            }
            else
            {
                switch (itemData.type)
                {
                    case ItemType.Weapon:
                        _uiSlot.equippedWeaponIndex = index;
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
                        _uiSlot.equippedArmorIndex = index;
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
        _uiSlot.RefreshUI();
    }
}