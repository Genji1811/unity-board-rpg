using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public PlayerController player;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI deckText;
    

    void Update()
    {
        if (player == null) return;

        hpText.text = $"HP: {player.currentHP} | AP: {player.currentAP}";
        deckText.text = $"Cards: {player.deck.Count}";

        string list = "";
        for (int i = 0; i < player.deck.Count; i++)
        {
            list += player.deck[i].cardName + "\n";
        }

    }
}