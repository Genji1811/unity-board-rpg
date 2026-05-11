using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DiceManager : MonoBehaviour
{
    public PlayerController player;

    public TextMeshProUGUI diceText;
    public Image diceImage;

    private Sprite[] diceSprites;

    void Start()
    {
        // load sprite từ Resources/DiceSides
        diceSprites = Resources.LoadAll<Sprite>("DiceSides");
    }

    public void RollDice()
    {
        if (player.isMoving)
        {
            Debug.Log("Still moving!");
            return;
        }

        StartCoroutine(RollAnimation());
    }

    IEnumerator RollAnimation()
    {
        // animation random
        for (int i = 0; i < 10; i++)
        {
            int temp = Random.Range(1, 7);

            diceText.text = "Dice: " + temp;

            diceImage.sprite = diceSprites[temp - 1];

            yield return new WaitForSeconds(0.05f);
        }

        // kết quả cuối
        int finalDice = Random.Range(1, 7);

        diceText.text = "Dice: " + finalDice;

        diceImage.sprite = diceSprites[finalDice - 1];

        Debug.Log("Dice: " + finalDice);

        player.Move(finalDice);
    }
}