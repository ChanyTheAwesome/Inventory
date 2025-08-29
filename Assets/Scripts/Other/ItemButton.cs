using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private ItemData[] itemDataArray;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        var character = GameManager.Instance.Character;
        var hasSpace = character.Inventory.Count + itemDataArray.Length <= character.MaxInventoryCount;
        
        if (!hasSpace || !character.TryPurchase(200)) return;
        foreach (var itemData in itemDataArray)
        {
            character.AddItem(itemData);
        }
    }
}