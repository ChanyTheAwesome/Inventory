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
        GameManager.Instance.Character.AddMoneyObserver(this);//옵저버를 달아줬습니다!
    }

    public void OnMoneyChanged(int money)
    {
        moneyText.text = $"{money:N0}";//보유 금액이 바뀔 때 마다 텍스트를 변경해줍니다!
        //이런 구조를 다른 곳에도 적용시키면 참 좋겠지만, 시간이 없었어요 ㅠㅠ
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
