using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    private int _slotCount;
    private readonly List<Slot> _slots = new List<Slot>();
    public int equippedWeaponIndex = -1;
    public int equippedArmorIndex = -1;

    [SerializeField] private TextMeshProUGUI currentSlotCountText;
    [SerializeField] private TextMeshProUGUI maxSlotCountText;
    [SerializeField] private GridLayoutGroup content;

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

    private void AddSlotOnLevelUp()
    {
        for (int i = _slots.Count; i < GameManager.Instance.Character.MaxInventoryCount; i++)
        {
            CreateSlot();
        }
        IncreaseHeight(content.spacing.y + content.cellSize.y);
        RefreshUI();
    }

    private void IncreaseHeight(float height)
    {
        var rt = content.GetComponent<RectTransform>();
        var currentHeight = rt.rect.height;
        var targetHeight = currentHeight + height;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
    }
    private void CreateSlot()
    {
        var go = Instantiate(slotPrefab, slotParent);
        
        var slot = go.GetComponent<Slot>();
        _slots.Add(slot);
        slot.Init(this, _slots.Count - 1);
    }

    public void RefreshUI()
    {
        foreach (var slot in _slots)
        {
            slot.SetUI();
        }
        currentSlotCountText.text = GameManager.Instance.Character.Inventory.Count.ToString();
        maxSlotCountText.text = _slots.Count.ToString();
    }
}
