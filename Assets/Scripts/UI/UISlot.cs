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
        GameManager.Instance.Character.OnItemAdded += RefreshUI;//아이템이 추가되면 RefreshUI를 호출하고,
    }

    private void OnEnable()
    {
        RefreshUI();
    }
    public void Init()
    {
        _slotCount = GameManager.Instance.Character.MaxInventoryCount;
        CreateSlots();
        GameManager.Instance.Character.OnLevelUp += AddSlotOnLevelUp;//레벨업 시에는 슬롯 추가가 활성화 될 것입니다.
    }

    private void CreateSlots()
    {
        for (var i = 0; i < _slotCount; i++)
        {
            CreateSlot();
        }
        RefreshUI();
    }

    private void AddSlotOnLevelUp()
    {
        for (var i = _slots.Count; i < GameManager.Instance.Character.MaxInventoryCount; i++)
        {
            CreateSlot();
        }
        //레벨업 시 늘어난 MaxInventoryCount의 수에 맞춰 슬롯을 더 생성하고
        IncreaseHeight(content.spacing.y + content.cellSize.y);//ScrollRect의 Content의 높이를 늘려줍니다!
        RefreshUI();
    }

    private void IncreaseHeight(float height)
    {
        var rt = content.GetComponent<RectTransform>();
        var currentHeight = rt.rect.height;//그냥 height 변경은 안되더라구요...
        var targetHeight = currentHeight + height;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);//그래서 이런 함수를 찾아봤습니다!
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);//또 이걸 쓰면 즉시 변경점이 적용된다고 하더라구요.
    }
    private void CreateSlot()
    {
        var go = Instantiate(slotPrefab, slotParent);
        
        var slot = go.GetComponent<Slot>();
        _slots.Add(slot);
        slot.Init(this, _slots.Count - 1);//슬롯 생성 후 slot의 Init 함수로 들어갑니다.
    }

    public void RefreshUI()
    {//Scroll Rect 안에 있는 모든 구성원들을 새로고침합니다.
        foreach (var slot in _slots)
        {
            slot.SetUI();//슬롯도 새로고침하구요.
        }
        currentSlotCountText.text = GameManager.Instance.Character.Inventory.Count.ToString();//현재 아이템 갯수와
        maxSlotCountText.text = _slots.Count.ToString();//최대 인벤토리 크기도 새로고침합니다!
    }
}
