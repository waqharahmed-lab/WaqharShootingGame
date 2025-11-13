using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

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

    public int coinValue = 1;
    private int totalCoins = 0;

    // === Life System ===
    private int lives = 3;
    private System.DateTime nextLifeTime;
    private const int maxLives = 3;
    private const int refillTimeMinutes = 1;


    public int coinsPerMinute = 1;         // how many coins to give per real minute
    public TextMeshProUGUI coinText;        // optional: assign in Inspector to show total coins


    void Start()
    {
        
        // Load lives and nextLifeTime
        lives = PlayerPrefs.GetInt("Lives", maxLives);
        string nextLifeString = PlayerPrefs.GetString("NextLifeTime", "");
        if (!string.IsNullOrEmpty(nextLifeString))
            nextLifeTime = System.DateTime.Parse(nextLifeString);

        // If lives are zero and timer not finished, go back to lobby
        if (lives <= 0 && System.DateTime.Now < nextLifeTime)
        {
            Debug.Log("❌ No lives left! Returning to Lobby.");
            SceneManager.LoadScene(0);
            return;
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
        isGameOver = false;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        score = 0;
        UpdateScoreText();
        UpdateHighScoreText();

        // 🪙 Auto coin generation (real time per minute)
        GiveTimedCoins();
        
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
            UpdateTotalCoins();

            if (finalScoreText != null)
                finalScoreText.text = "Final Score: " + score;

            if (finalHighScoreText != null)
                finalHighScoreText.text = "High Score: " + highScore;
        }
    }
    

    public void RestartGame()
    {
        // Deduct a life on Restart
        lives = PlayerPrefs.GetInt("Lives", maxLives);
        lives = Mathf.Max(0, lives - 1);
        PlayerPrefs.SetInt("Lives", lives);

        // If no lives remain, start timer and go to Lobby
        if (lives <= 0)
        {
            nextLifeTime = System.DateTime.Now.AddMinutes(refillTimeMinutes);
            PlayerPrefs.SetString("NextLifeTime", nextLifeTime.ToString());
            PlayerPrefs.Save();

            Debug.Log("No lives left! Returning to Lobby.");
            SceneManager.LoadScene(0);
            return;
        }

        PlayerPrefs.Save();
        Debug.Log("Restarting game... Lives left: " + lives);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreText();
        }
    }

    private void UpdateTotalCoins()
    {
        int earnedCoins = score * coinValue;
        totalCoins += earnedCoins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
        Debug.Log("Earned " + earnedCoins + " coins! Total now: " + totalCoins);

        if (coinText != null)
            coinText.text = "Coins: " + totalCoins;
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

    // 🪙 Real-time coin system (per minute)
    private void GiveTimedCoins()
    {
        string lastLogin = PlayerPrefs.GetString("LastLoginTime", "");
        DateTime now = DateTime.Now;

        int coinsEarned = 0;

        if (!string.IsNullOrEmpty(lastLogin))
        {
            DateTime lastLoginTime = DateTime.Parse(lastLogin);
            TimeSpan timePassed = now - lastLoginTime;

            int minutesPassed = Mathf.FloorToInt((float)timePassed.TotalMinutes);

            if (minutesPassed >= 5)
            {
                coinsEarned = minutesPassed * coinsPerMinute;
                totalCoins += coinsEarned;

                PlayerPrefs.SetInt("TotalCoins", totalCoins);
                PlayerPrefs.SetInt("LastEarnedCoins", coinsEarned); // ✅ Save how many coins were earned
                PlayerPrefs.Save();
            }
        }

        // ✅ Update last login time
        PlayerPrefs.SetString("LastLoginTime", now.ToString());
        PlayerPrefs.Save();

        // ✅ Update UI if assigned
        if (coinText != null)
            coinText.text = "Coins: " + totalCoins;
    }
    public void AddCoins(int amount)
{
    totalCoins += amount;
    PlayerPrefs.SetInt("TotalCoins", totalCoins);
    PlayerPrefs.Save();
    if (coinText != null)
        coinText.text = "Coins: " + totalCoins;
}

}
