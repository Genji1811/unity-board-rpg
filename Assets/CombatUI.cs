using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CombatUI : MonoBehaviour
{
    public GameObject panel;
    public PlayerController player;
    public TMP_Text enemyText;
    public TMP_Text diceText;
    public TMP_Text damageText;
    public TMP_Text resultText;
    public Transform cardContainer;
    public GameObject cardPrefab;
    
    public Button fightButton;
    public Button endButton;
    public Image diceImage;

    private System.Action onEnd;
    private System.Action onFight;
    private Sprite[] diceSprites;
    public void Show(int enemyHP, System.Action fightAction)
    {
        RefreshCards();
        panel.SetActive(true);
        resultImage.gameObject.SetActive(false);
        enemyText.text = "Enemy HP: " + enemyHP;
        diceText.text = "";
        damageText.text = "";
        resultText.text = "";

        onFight = fightAction;

        fightButton.onClick.RemoveAllListeners();
        fightButton.onClick.AddListener(() => OnFightClick());
        endButton.gameObject.SetActive(false);
    }
    void Start()
    {
        panel.SetActive(false);
        diceSprites = Resources.LoadAll<Sprite>("DiceSides");

        endButton.gameObject.SetActive(false);
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
    public Sprite WinSprite;
    public Sprite LoseSprite;
    public Image resultImage;
    
    public void ShowResult(bool win)
    {
        resultImage.gameObject.SetActive(true);

        resultImage.sprite = win ? WinSprite : LoseSprite;
    }

    public void Hide()
    {
        panel.SetActive(false);
        resultImage.gameObject.SetActive(false);
    }
    public void RefreshCards()
{
    foreach (Transform child in cardContainer)
    {
        Destroy(child.gameObject);
    }

    foreach (Card card in player.deck)
    {
        GameObject obj =
            Instantiate(cardPrefab, cardContainer);

        CardUI ui =
            obj.GetComponent<CardUI>();

        ui.Setup(card, player, this);
    }
}
    public IEnumerator RollDiceAnimation(System.Action<int> onFinish)
{
    for (int i = 0; i < 10; i++)
    {
        int temp = Random.Range(1, 7);

        diceText.text = "Dice: " + temp;

        diceImage.sprite = diceSprites[temp - 1];

        yield return new WaitForSeconds(0.05f);
    }

    int finalDice = Random.Range(1, 7);

    diceText.text = "Dice: " + finalDice;

    diceImage.sprite = diceSprites[finalDice - 1];

    onFinish?.Invoke(finalDice);
}
public void SetupEndButton(System.Action endAction)
{
    onEnd = endAction;

    endButton.gameObject.SetActive(true);

    endButton.onClick.RemoveAllListeners();

    endButton.onClick.AddListener(() =>
    {
        onEnd?.Invoke();
    });
}
}