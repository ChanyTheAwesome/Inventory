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
        if (GameManager.Instance.Character.Inventory.Count > index)//인벤토리에 해당 인덱스 아이템이 있는지 우선 확인하고
        {
            itemData = GameManager.Instance.Character.Inventory[index];//여러 값들을 넣어준 뒤,
            image.sprite = itemData.icon;
            button.gameObject.SetActive(true);//버튼을 활성화합니다.
        }
        else
        {
            button.gameObject.SetActive(false);//없다면 버튼을 활성화하지 않습니다.
        }

        if (_uiSlot == null) return;

        if(_uiSlot.equippedWeaponIndex == index || _uiSlot.equippedArmorIndex == index) //장착한 아이템이라면
        {
            equippedText.gameObject.SetActive(true);//E 라고 적힌 텍스트를 활성화합니다.
        }
        else
        {
            equippedText.gameObject.SetActive(false);//아니면 비활성화
        }
    }

    private void OnClickSlot()
    {
        if (itemData.isEquippable)//현재는 장착 가능한 아이템만 있지만... 혹시 모르잖아요!
        {//우선 장착된 아이템인지 확인하고, 맞다면 각 타입에 맞는 비활성화 작업을 수행합니다.
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
                    default:
                        Debug.LogError("Wrong item type");
                        break;
                }
            }
        }
        _uiSlot.RefreshUI();
    }
}