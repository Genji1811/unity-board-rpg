using UnityEngine;
using TMPro;

public class DiceManager : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI diceText;

    public void RollDice()
    {
        int dice = Random.Range(1, 4);

        diceText.text = "Dice: " + dice;

        Debug.Log("Dice rolled: " + dice);

        player.Move(dice);
    }
}
