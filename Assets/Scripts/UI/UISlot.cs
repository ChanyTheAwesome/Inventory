using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private int defaultSlotCount = 20;
    private int _slotCount;
    [HideInInspector] public int AdditionalSlotCount { get; set; }
    private List<Slot> _slots = new List<Slot>();
    public int equippedWeaponIndex;
    public int equippedArmorIndex;
    public void Init() 
    {
        _slotCount = defaultSlotCount + AdditionalSlotCount;
        CreateSlots();
        GameManager.Instance.Character.OnLevelUp += AddSlotOnLevelUp;
    }

    private void CreateSlots()
    {
        for (int i = 0; i < _slotCount; i++)
        {
            CreateSlot();
        }
    }

    public void AddSlotOnLevelUp()
    {
        for (int i = 0; i < 5; i++)
        {
            CreateSlot();
        }
    }
    
    private void CreateSlot()
    {
        GameObject go = Instantiate(slotPrefab, slotParent);
        
        Slot slot = go.GetComponent<Slot>();
        _slots.Add(slot);
        slot.Init(this, _slots.Count - 1);
    }
    
}
