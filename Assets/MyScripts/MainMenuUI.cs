using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI messageText;
    public UnityEngine.UI.Image[] hearts;
    public TextMeshProUGUI timerText;

  
    public UnityEngine.UI.Button rewardButton; 
    public int dailyRewardCoins = 200;        

    public int entryFee = 10;
    private const int maxLives = 3;
    private const int refillTimeMinutes = 1;

    private int totalCoins;
    private int lives;
    private System.DateTime nextLifeTime;

    void Start()
    {
        LoadLives();
        UpdateHeartsUI();
        UpdateCoinDisplay();
        CheckDailyRewardButton();
    }

   
    public void OnPlayButton()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        lives = PlayerPrefs.GetInt("Lives", maxLives);

        if (lives <= 0)
        {
            if (messageText != null)
                messageText.text = "No lives left! Wait for refill.";
            return;
        }

        if (totalCoins >= entryFee)
        {
            totalCoins -= entryFee;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            PlayerPrefs.Save();

            UpdateCoinDisplay();
            SceneManager.LoadScene(1); // Start game
        }
        else
        {
            if (messageText != null)
                messageText.text = "Not enough coins!";
        }
    }

    // ✅ Coin display
    private void UpdateCoinDisplay()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        if (coinText != null)
            coinText.text = totalCoins.ToString();
    }

    // ✅ Cheat to add coins
    public void OnCheatAddCoins()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += 100;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();

        Debug.Log("Cheat used! Coins increased to: " + totalCoins);
        UpdateCoinDisplay();

        if (messageText != null)
            messageText.text = "+100 coins added!";
    }

    // ✅ Load & Save lives
    private void LoadLives()
    {
        lives = PlayerPrefs.GetInt("Lives", maxLives);
        string nextLifeString = PlayerPrefs.GetString("NextLifeTime", "");
        if (!string.IsNullOrEmpty(nextLifeString))
            nextLifeTime = System.DateTime.Parse(nextLifeString);
    }

    private void SaveLives()
    {
        PlayerPrefs.SetInt("Lives", lives);
        if (lives < maxLives)
            PlayerPrefs.SetString("NextLifeTime", nextLifeTime.ToString());
        PlayerPrefs.Save();
    }

    // ✅ Heart UI + Timer
    private void UpdateHeartsUI()
    {
        if (hearts == null || hearts.Length == 0)
            return;

        // Show hearts based on remaining lives
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
                hearts[i].enabled = i < lives;
        }

        if (timerText == null)
            return;

        // Show timer only when lives are zero
        if (lives <= 0)
        {
            System.TimeSpan timeLeft = nextLifeTime - System.DateTime.Now;
            if (timeLeft.TotalSeconds > 0)
            {
                timerText.text = "Next life in: " +
                    timeLeft.Minutes.ToString("00") + ":" +
                    timeLeft.Seconds.ToString("00");
            }
            else
            {
                // Refill all lives after timer ends
                lives = maxLives;
                PlayerPrefs.SetInt("Lives", lives);
                PlayerPrefs.SetString("NextLifeTime", "");
                PlayerPrefs.Save();
                timerText.text = "";
            }
        }
        else
        {
            timerText.text = "";
        }
    }

    void Update()
    {
        if (lives < maxLives)
            UpdateHeartsUI();
    }


    public void OnDailyRewardButton()
    {
        string lastClaimDate = PlayerPrefs.GetString("LastRewardDate", "");
        string todayDate = System.DateTime.Now.ToString("yyyy-MM-dd");

        if (lastClaimDate != todayDate)
        {
            // First claim today
            totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
            totalCoins += dailyRewardCoins;

            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            PlayerPrefs.SetString("LastRewardDate", todayDate);
            PlayerPrefs.Save();

            UpdateCoinDisplay();

            Debug.Log($"Daily reward claimed! +{dailyRewardCoins} coins");

            if (messageText != null)
                messageText.text = $"You received {dailyRewardCoins} coins!";
            if (rewardButton != null)
                rewardButton.interactable = false;
        }
        else
        {
            if (messageText != null)
                messageText.text = "Already claimed today's reward.";
            if (rewardButton != null)
                rewardButton.interactable = false;
        }
    }

    private void CheckDailyRewardButton()
    {
        string lastClaimDate = PlayerPrefs.GetString("LastRewardDate", "");
        string todayDate = System.DateTime.Now.ToString("yyyy-MM-dd");

        if (lastClaimDate == todayDate)
        {
            if (rewardButton != null)
                rewardButton.interactable = false;
            if (messageText != null)
                messageText.text = "Daily reward already claimed.";
        }
        else
        {
            if (rewardButton != null)
                rewardButton.interactable = true;
            if (messageText != null)
                messageText.text = "Tap to claim your daily reward!";
        }
    }
}
