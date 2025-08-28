using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum UIType
{
    Inventory,
    Status,
    MainMenu,
}
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [HideInInspector]
    public static UIManager Instance => _instance;
    
    [HideInInspector] public UIType currentUIType;
    private readonly Dictionary<UIType, GameObject> _uiDict = new();
    
    [SerializeField] private GameObject uiInventory;
    public GameObject UIInventory => uiInventory;
    
    [SerializeField] private GameObject uiStatus;
    public GameObject UIStatus => uiStatus;
    
    [SerializeField] private GameObject uiMainMenu;
    [SerializeField] private GameObject uiDefault;
    public GameObject UIMainMenu => uiMainMenu;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    public void Init()
    {
        SetUI(UIType.MainMenu);
        InitUIDict();
        uiDefault.GetComponent<UIDefault>().Init();
        uiInventory.GetComponent<UIInventory>().Init();
    }
    private void InitUIDict()
    {
        _uiDict.Add(UIType.Inventory, uiInventory);
        _uiDict.Add(UIType.Status, uiStatus);
        _uiDict.Add(UIType.MainMenu, uiMainMenu);
    }
    
    public void SetUI(UIType type)
    {
        currentUIType = type;
        ChangeUI(currentUIType);
    }

    private void ChangeUI(UIType type)
    {
        foreach (var UI in _uiDict)
        {
            UI.Value.SetActive(UI.Key == type);
        }
    }
}
