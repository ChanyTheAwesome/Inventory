using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{//������ �̷����� "������" ���𰡰� ��� ���� ĳ���� ������� �Ǿ����ϴ�.
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public Character Character;
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
    {//���� ������ ���� ������ ��������. ĳ���Ͱ� ���� �����ǰ�, �� ���Ŀ� UI�� ������ �ʱ�ȭ�˴ϴ�.
        SetData();
        UIManager.Instance.Init();
    }

    private void SetData()
    {
        Character = new Character(1, 0, 35, 40, 100, 25, 20000);
    }
}
