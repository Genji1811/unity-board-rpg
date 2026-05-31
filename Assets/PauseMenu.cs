using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public BoardManager boardManager;
    public PlayerController player;

    void Start()
    {
        menuPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        menuPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
    public void NewMap()
{
    boardManager.GenerateBoard();

    player.currentTileIndex = 0;

    player.transform.position =
        player.tiles[0].position;

    menuPanel.SetActive(false);
}

    public void QuitGame()
    {
        Application.Quit();
    }
}