using UnityEngine;
using TMPro;

public class DiceManager : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI diceText;

    public void RollDice()
    {
        
        int dice = Random.Range(1, 4);
        Debug.Log("Dice: " + dice);

        player.Move(dice);
        diceText.text = "Dice: " + dice;
        if (player.isMoving)
        {
            Debug.Log("Still moving!");
            return;
        }
    }
}
