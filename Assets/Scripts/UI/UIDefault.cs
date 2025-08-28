using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDefault : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    
    public void Init()
    {
        UpdateUI(GameManager.Instance.Character);
    }
    private void UpdateUI(Character character)
    {
        if (character == null) return;
        moneyText.text = $"{character.Money:N0}";
        levelText.text = $"Lv.{character.Level}";
        expText.text = $"EXP: {character.Exp} / {character.Level*3}";
    }
}
