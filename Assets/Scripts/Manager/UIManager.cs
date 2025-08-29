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
    [HideInInspector] public static UIManager Instance => _instance;
    
    [HideInInspector] public UIType currentUIType;
    private Dictionary<UIType, GameObject> _uiDict = new();
    
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
        InitUIDict();
        SetUI(UIType.MainMenu);//이후 메인 메뉴를 먼저 띄웁니다.
        uiDefault.GetComponent<UIDefault>().Init();
        uiInventory.GetComponent<UIInventory>().Init();
    }
    private void InitUIDict()//사전에 각 UIType을 맞춰놓습니다.
    {
        if (_uiDict == null)//혹시나 사전이 없다면
        {
            _uiDict = new Dictionary<UIType, GameObject>();//새로 생성하고
        }
        else
        {//그렇지 않다면 초기화를 시킵니다. 혹여나 두 번 불리게 될 수도 있으니까요.
            _uiDict.Clear();
        }
        _uiDict.Add(UIType.Inventory, uiInventory);
        _uiDict.Add(UIType.Status, uiStatus);
        _uiDict.Add(UIType.MainMenu, uiMainMenu);
    }
    
    public void SetUI(UIType type)
    {//currentUIType을 조정하고, 이에 맞춰 ChangeUI 메서드로 넘어갑니다.
        currentUIType = type;
        ChangeUI(currentUIType);
    }

    private void ChangeUI(UIType type)
    {//foreach를 돌면서 type에 맞는 UI를 활성화합니다!
        foreach (var ui in _uiDict)
        {
            ui.Value.SetActive(ui.Key == type);
        }
    }
}
