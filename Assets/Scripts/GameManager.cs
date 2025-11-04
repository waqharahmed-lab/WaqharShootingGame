using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;   // ✅ this must be here!

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;   // ✅ must be this type for TMP
    private int score = 0;
    private bool isGameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        score = 0;
        UpdateScoreText();
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("GAME OVER!");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
