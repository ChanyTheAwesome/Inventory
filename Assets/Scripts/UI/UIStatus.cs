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
    
    private void Start()
    {
        backButton.onClick.AddListener(OnClickBack);
    }

    private void OnEnable()
    {
        UpdateStatus(GameManager.Instance.Character);
    }
    private void UpdateStatus(Character character)
    {
        if (character == null) return;
        attackText.text = $"{character.FinalAttackDmg}";
        armorText.text = $"{character.FinalArmor}";
        healthText.text = $"{character.Health}";
        criticalText.text = $"{character.CriticalRate}%";
    }

    private void OnClickBack()
    {
        UIManager.Instance.SetUI(UIType.MainMenu);
    }
}
