using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;   // ✅ Required for TextMeshProUGUI

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI highScoreText;   
    public TextMeshProUGUI finalScoreText;  
    public TextMeshProUGUI finalHighScoreText; 

    private int score = 0;
    private int highScore = 0;
    private bool isGameOver = false;

    void Start()
    {
        // ✅ Hide GameOver panel at start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
        isGameOver = false;

        // ✅ Load saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // ✅ Reset score display
        score = 0;
        UpdateScoreText();
        UpdateHighScoreText();
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("GAME OVER!");

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            Time.timeScale = 0f;

            if (finalScoreText != null)
                finalScoreText.text = "Final Score: " + score;

            if (finalHighScoreText != null)
                finalHighScoreText.text = "High Score: " + highScore;
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

        // ✅ Update high score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreText();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
            highScoreText.text = "High: " + highScore;
    }
}
