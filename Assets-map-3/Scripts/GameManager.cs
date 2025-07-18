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
    public TextMeshProUGUI pauseGameText;

    public GameObject player;
    public GameObject pauseGameCanvas;
    public Button restartButton;
    public Button retryButton;
    public Button exitButton;

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

        if (pauseGameText != null)
            pauseGameText.gameObject.SetActive(false);

        if (pauseGameCanvas != null)
            pauseGameCanvas.SetActive(false);

        if (restartButton != null)
            restartButton.gameObject.SetActive(false);

        if (exitButton != null)
            exitButton.gameObject.SetActive(false);

        if (retryButton != null)
            exitButton.gameObject.SetActive(false);
            


        if (scoreText != null)
            scoreText.gameObject.SetActive(true);

        if (player != null)
            player.SetActive(true);
    }

  
    private bool isPaused = false;

private void Update()
{
    if (Input.GetKeyDown(KeyCode.J))
    {
        isPaused = !isPaused;

        if (pauseGameCanvas != null)
            pauseGameCanvas.SetActive(isPaused);

        if (pauseGameText != null)
            pauseGameText.gameObject.SetActive(isPaused);

        if (restartButton != null)
            restartButton.gameObject.SetActive(isPaused);

        if (exitButton != null)
            exitButton.gameObject.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }
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
            retryButton.gameObject.SetActive(true);

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
