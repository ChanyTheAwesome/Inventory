using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private UISlot uiSlot;
    
    private void Start()
    {
        backButton.onClick.AddListener(OnClickBack);
    }

    public void Init()
    {
        uiSlot.Init();
    }
    private static void OnClickBack()
    {
        UIManager.Instance.SetUI(UIType.MainMenu);
    }
}
