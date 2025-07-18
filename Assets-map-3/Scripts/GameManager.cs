using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameWinText;
    public TextMeshProUGUI scoreText;
    public GameObject player;
    public Button restartButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);

        if (gameWinText != null)
            gameWinText.gameObject.SetActive(false);

        if (restartButton != null)
            restartButton.gameObject.SetActive(false);

        if (scoreText != null)
            scoreText.gameObject.SetActive(true);

        if (player != null)
            player.SetActive(true);
    }

    public void GameOver()
{
    if (gameOverText != null)
        gameOverText.gameObject.SetActive(true);

    EndGameCommon();
}

public void GameWinner()
{
    if (gameWinText != null)
        gameWinText.gameObject.SetActive(true);

    EndGameCommon();
}
    private void EndGameCommon()
{
    if (restartButton != null)
        restartButton.gameObject.SetActive(true);

    if (scoreText != null)
        scoreText.gameObject.SetActive(true);

    if (player != null)
        player.SetActive(true);

    Time.timeScale = 0f;
}


    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
