using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    public static WinLoseUI instance;

    public GameObject panel;

    public TMP_Text resultText;
    public TMP_Text turnText;

    public Image resultImage;

    public Sprite winSprite;
    public Sprite loseSprite;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        panel.SetActive(false);
    }

    public void ShowWin(int turns)
    {
        panel.SetActive(true);

        resultText.text = "YOU WIN!";
        turnText.text = "Turns: " + turns;

        resultImage.sprite = winSprite;
    }

    public void ShowLose()
    {
        panel.SetActive(true);

        resultText.text = "GAME OVER";
        turnText.text = "";

        resultImage.sprite = loseSprite;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}