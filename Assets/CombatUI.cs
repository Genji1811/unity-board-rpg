using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    public GameObject panel;
    public PlayerController player;
    public TMP_Text enemyText;
    public TMP_Text diceText;
    public TMP_Text damageText;
    public TMP_Text resultText;
    public Transform cardContainer;
    public GameObject cardButtonPrefab;
    
    public Button fightButton;

    private System.Action onFight;

    public void Show(int enemyHP, System.Action fightAction)
    {
        ShowCards();
        panel.SetActive(true);
        
        enemyText.text = "Enemy HP: " + enemyHP;
        diceText.text = "";
        damageText.text = "";
        resultText.text = "";

        onFight = fightAction;

        fightButton.onClick.RemoveAllListeners();
        fightButton.onClick.AddListener(() => OnFightClick());
    }
    void Start()
    {
        panel.SetActive(false);
    }
    void OnFightClick()
    {
        onFight?.Invoke();
    }

    public void UpdateCombat(int dice, int ap)
    {   

        diceText.text = "Dice: " + dice;
        damageText.text = "Damage: " + dice + " × " + ap + " = " + (dice * ap);
    }

    public void ShowResult(bool win)
    {
        resultText.text = win ? "WIN!" : "LOSE!";
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
    void ShowCards()
{
    foreach (Transform child in cardContainer)
        Destroy(child.gameObject);

    foreach (Card card in player.deck)
    {
        GameObject btn = Instantiate(cardButtonPrefab, cardContainer);

        btn.GetComponentInChildren<TMP_Text>().text = card.cardName;

        btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            player.UseCard(card);
            ShowCards(); // refresh UI
        });
    }
}
}