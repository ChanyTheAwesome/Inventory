using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private int defaultSlotCount = 20;
    private int _slotCount;
    private readonly List<Slot> _slots = new List<Slot>();
    public int equippedWeaponIndex = -1;
    public int equippedArmorIndex = -1;

    [SerializeField] private TextMeshProUGUI currentSlotCountText;
    [SerializeField] private TextMeshProUGUI maxSlotCountText;

    private void Start()
    {
        GameManager.Instance.Character.OnItemAdded += RefreshUI;
    }

    private void OnEnable()
    {
        RefreshUI();
    }
    public void Init()
    {
        _slotCount = GameManager.Instance.Character.MaxInventoryCount;
        CreateSlots();
        GameManager.Instance.Character.OnLevelUp += AddSlotOnLevelUp;
    }

    private void CreateSlots()
    {
        for (int i = 0; i < _slotCount; i++)
        {
            CreateSlot();
        }
        RefreshUI();
    }

    public void AddSlotOnLevelUp()
    {
        for (int i = _slots.Count; i < GameManager.Instance.Character.MaxInventoryCount; i++)
        {
            CreateSlot();
        }
        RefreshUI();
    }
    
    private void CreateSlot()
    {
        GameObject go = Instantiate(slotPrefab, slotParent);
        
        Slot slot = go.GetComponent<Slot>();
        _slots.Add(slot);
        slot.Init(this, _slots.Count - 1);
    }

    public void RefreshUI()
    {
        foreach (Slot slot in _slots)
        {
            slot.SetUI();
        }
        currentSlotCountText.text = GameManager.Instance.Character.Inventory.Count.ToString();
        maxSlotCountText.text = _slots.Count.ToString();
    }
}
