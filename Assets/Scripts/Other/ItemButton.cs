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
    {//버튼을 누르면
        var character = GameManager.Instance.Character;
        var hasSpace = character.Inventory.Count + itemDataArray.Length <= character.MaxInventoryCount;
        
        if (!hasSpace || !character.TryPurchase(200)) return;
        //공간이 있는지 "먼저" 확인하고, 공간이 있다면 구매 시도를 합니다.
        foreach (var itemData in itemDataArray)
        {
            character.AddItem(itemData);
        }
        //구매 완료시 아이템을 인벤토리에 넣습니다!
    }
}