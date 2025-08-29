using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private ItemData[] itemDatas;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }
    private void OnClickButton()
    {
        Character character = GameManager.Instance.Character;
        bool hasSpace = character.Inventory.Count + itemDatas.Length <= character.MaxInventoryCount;
        if (hasSpace && character.TryPurchase(200))
        {
            foreach (ItemData itemData in itemDatas)
            {
                character.AddItem(itemData);
            }
        }
    }
}
