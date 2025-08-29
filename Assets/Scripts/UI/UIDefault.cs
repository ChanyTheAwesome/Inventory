using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDefault : MonoBehaviour, IMoneyObserver
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Button giveItemButton;
    [SerializeField] private Button giveExpButton;
    [SerializeField] private Image expImage;
    public void Init()
    {
        UpdateUI(GameManager.Instance.Character);
        giveExpButton.onClick.AddListener(AddOneExp);
        expImage.fillAmount = GameManager.Instance.Character.Exp / (float)GameManager.Instance.Character.Level * 3;
        GameManager.Instance.Character.AddMoneyObserver(this);//�������� �޾�����ϴ�!
    }

    public void OnMoneyChanged(int money)
    {
        moneyText.text = $"{money:N0}";//���� �ݾ��� �ٲ� �� ���� �ؽ�Ʈ�� �������ݴϴ�!
        //�̷� ������ �ٸ� ������ �����Ű�� �� ��������, �ð��� ������� �Ф�
    }
    private void UpdateUI(Character character)
    {
        if (character == null) return;
        moneyText.text = $"{character.Money:N0}";
        levelText.text = $"Lv.{character.Level}";
        expText.text = $"EXP: {character.Exp} / {character.Level*3}";
    }

    private void AddOneExp()
    {
        GameManager.Instance.Character.AddExp(1);
        expImage.fillAmount = (float)GameManager.Instance.Character.Exp / (GameManager.Instance.Character.Level * 3);
        UpdateUI(GameManager.Instance.Character);
    }
}
