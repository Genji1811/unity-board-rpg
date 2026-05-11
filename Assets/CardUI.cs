using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descText;

    public Button button;
    public Image bg;

    private Card card;
    private PlayerController player;
    private CombatUI combatUI;

    public void Setup(
        Card newCard,
        PlayerController p,
        CombatUI ui
    )
    {
        card = newCard;
        player = p;
        combatUI = ui;

        nameText.text = card.cardName;

        descText.text = card.description;


        switch(card.type)
        {
            case CardType.Attack:
                nameText.color = Color.red;
                break;

            case CardType.Defense:
                nameText.color = Color.blue;
                break;

            case CardType.Special:
                nameText.color = Color.green;
                break;
        }

        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(UseCard);
    }

    void UseCard()
    {
        player.UseCard(card);

        combatUI.RefreshCards();
    }
}