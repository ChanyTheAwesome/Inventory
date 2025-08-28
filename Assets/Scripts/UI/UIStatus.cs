using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI criticalText;
    [SerializeField] private Button backButton;
    private Character character;
    
    private void Start()
    {
        character = GameManager.Instance.Character;
        backButton.onClick.AddListener(OnClickBack);
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        attackText.text = $"{character.AttackDmg}";
        armorText.text = $"{character.Armor}";
        healthText.text = $"{character.Health}";
        criticalText.text = $"{character.CriticalRate}%";
    }

    private void OnClickBack()
    {
        UIManager.Instance.SetUI(UIType.MainMenu);
    }
}
