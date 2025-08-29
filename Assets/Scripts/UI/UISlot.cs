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
        GameManager.Instance.Character.OnItemAdded += RefreshUI;//�������� �߰��Ǹ� RefreshUI�� ȣ���ϰ�,
    }

    private void OnEnable()
    {
        RefreshUI();
    }
    public void Init()
    {
        _slotCount = GameManager.Instance.Character.MaxInventoryCount;
        CreateSlots();
        GameManager.Instance.Character.OnLevelUp += AddSlotOnLevelUp;//������ �ÿ��� ���� �߰��� Ȱ��ȭ �� ���Դϴ�.
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
        //������ �� �þ MaxInventoryCount�� ���� ���� ������ �� �����ϰ�
        IncreaseHeight(content.spacing.y + content.cellSize.y);//ScrollRect�� Content�� ���̸� �÷��ݴϴ�!
        RefreshUI();
    }

    private void IncreaseHeight(float height)
    {
        var rt = content.GetComponent<RectTransform>();
        var currentHeight = rt.rect.height;//�׳� height ������ �ȵǴ��󱸿�...
        var targetHeight = currentHeight + height;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);//�׷��� �̷� �Լ��� ã�ƺý��ϴ�!
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);//�� �̰� ���� ��� �������� ����ȴٰ� �ϴ��󱸿�.
    }
    private void CreateSlot()
    {
        var go = Instantiate(slotPrefab, slotParent);
        
        var slot = go.GetComponent<Slot>();
        _slots.Add(slot);
        slot.Init(this, _slots.Count - 1);//���� ���� �� slot�� Init �Լ��� ���ϴ�.
    }

    public void RefreshUI()
    {//Scroll Rect �ȿ� �ִ� ��� ���������� ���ΰ�ħ�մϴ�.
        foreach (var slot in _slots)
        {
            slot.SetUI();//���Ե� ���ΰ�ħ�ϱ���.
        }
        currentSlotCountText.text = GameManager.Instance.Character.Inventory.Count.ToString();//���� ������ ������
        maxSlotCountText.text = _slots.Count.ToString();//�ִ� �κ��丮 ũ�⵵ ���ΰ�ħ�մϴ�!
    }
}
