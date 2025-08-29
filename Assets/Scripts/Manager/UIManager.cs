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
        SetUI(UIType.MainMenu);//���� ���� �޴��� ���� ���ϴ�.
        uiDefault.GetComponent<UIDefault>().Init();
        uiInventory.GetComponent<UIInventory>().Init();
    }
    private void InitUIDict()//������ �� UIType�� ��������ϴ�.
    {
        if (_uiDict == null)//Ȥ�ó� ������ ���ٸ�
        {
            _uiDict = new Dictionary<UIType, GameObject>();//���� �����ϰ�
        }
        else
        {//�׷��� �ʴٸ� �ʱ�ȭ�� ��ŵ�ϴ�. Ȥ���� �� �� �Ҹ��� �� ���� �����ϱ��.
            _uiDict.Clear();
        }
        _uiDict.Add(UIType.Inventory, uiInventory);
        _uiDict.Add(UIType.Status, uiStatus);
        _uiDict.Add(UIType.MainMenu, uiMainMenu);
    }
    
    public void SetUI(UIType type)
    {//currentUIType�� �����ϰ�, �̿� ���� ChangeUI �޼���� �Ѿ�ϴ�.
        currentUIType = type;
        ChangeUI(currentUIType);
    }

    private void ChangeUI(UIType type)
    {//foreach�� ���鼭 type�� �´� UI�� Ȱ��ȭ�մϴ�!
        foreach (var ui in _uiDict)
        {
            ui.Value.SetActive(ui.Key == type);
        }
    }
}
