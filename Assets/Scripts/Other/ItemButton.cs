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
        foreach (ItemData itemData in itemDatas)
        {
            GameManager.Instance.Character.AddItem(itemData);
        }
    }
}
