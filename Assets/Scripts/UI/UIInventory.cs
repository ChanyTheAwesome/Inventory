using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Button backButton;
    
    private void Start()
    {
        backButton.onClick.AddListener(OnClickBack);
    }
    public void OnClickBack()
    {
        UIManager.Instance.SetUI(UIType.MainMenu);
    }
}
