using System;
using System.Collections;
using System.Collections.Generic;
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

    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetUI(UIType.MainMenu);
        InitUIDict();
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
