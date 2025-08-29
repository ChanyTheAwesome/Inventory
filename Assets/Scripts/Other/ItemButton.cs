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
        if (GameManager.Instance.Character.TryPurchase(200) &&
            GameManager.Instance.Character.Inventory.Count <= GameManager.Instance.Character.MaxInventoryCount + itemDatas.Length)
        {
            foreach (ItemData itemData in itemDatas)
            {
                GameManager.Instance.Character.AddItem(itemData);
            }
        }
    }
}
