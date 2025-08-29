using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;

    private void Start()
    {
        statusButton.onClick.AddListener(OnClickStatus);
        inventoryButton.onClick.AddListener(OnClickInventory);
    }

    private static void OnClickStatus()
    {
        UIManager.Instance.SetUI(UIType.Status);
    }
    
    private static void OnClickInventory()
    {
        UIManager.Instance.SetUI(UIType.Inventory);
    }
}
