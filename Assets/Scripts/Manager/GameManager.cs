using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{//지금은 이렇다할 "게임적" 무언가가 없어서 그저 캐릭터 저장용이 되었습니다.
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
    {//물론 데이터 세팅 순서는 맞춰놨어요. 캐릭터가 먼저 생성되고, 그 이후에 UI의 값들이 초기화됩니다.
        SetData();
        UIManager.Instance.Init();
    }

    private void SetData()
    {
        Character = new Character(1, 0, 35, 40, 100, 25, 20000);
    }
}
