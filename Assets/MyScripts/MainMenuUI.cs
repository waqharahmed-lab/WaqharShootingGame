using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI messageText;
    public UnityEngine.UI.Image[] hearts;
    public TextMeshProUGUI timerText; // Life refill timer
    public TextMeshProUGUI dailyRewardTimerText; // Daily reward cooldown timer
    public TextMeshProUGUI hourlyRewardTimerText; // Hourly reward timer

    [Header("Buttons & Rewards")]
    public UnityEngine.UI.Button rewardButton; // Daily reward
    public UnityEngine.UI.Button hourlyRewardButton; // Hourly reward button
    public int dailyRewardCoins = 200;
    public float dailyRewardCooldownHours = 24f;
    public int hourlyRewardCoins = 100;
    public float hourlyRewardCooldownHours = 1f;

    [Header("Game Settings")]
    public int entryFee = 200;
    private const int maxLives = 3;

    private int totalCoins;
    private int lives;
    private System.DateTime nextLifeTime;
    private System.DateTime nextRewardTime;
    private System.DateTime nextHourlyRewardTime;

    void Start()
    {
        LoadLives();
        UpdateHeartsUI();
        UpdateCoinDisplay();
        LoadDailyRewardTimer();
        LoadHourlyRewardTimer();
    }

    void Update()
    {
        if (lives < maxLives)
            UpdateHeartsUI();

        UpdateDailyRewardTimerUI();
        UpdateHourlyRewardTimerUI();
    }

    // ---------------- PLAY BUTTON ----------------
    public void OnPlayButton()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        lives = PlayerPrefs.GetInt("Lives", maxLives);

        if (lives <= 0)
        {
            messageText.text = "No lives left! Wait for refill.";
            return;
        }

        if (totalCoins >= entryFee)
        {
            totalCoins -= entryFee;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            PlayerPrefs.Save();
            UpdateCoinDisplay();
            SceneManager.LoadScene(1);
        }
        else
        {
            messageText.text = "Not enough coins!";
        }
    }

    // ---------------- COINS ----------------
    private void UpdateCoinDisplay()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        if (coinText != null)
            coinText.text = totalCoins.ToString();
    }

    public void OnCheatAddCoins()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += 100;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();

        UpdateCoinDisplay();
        messageText.text = "+100 coins added!";
    }

    // ---------------- LIVES ----------------
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

    private void UpdateHeartsUI()
    {
        if (hearts == null || hearts.Length == 0)
            return;

        for (int i = 0; i < hearts.Length; i++)
            hearts[i].enabled = i < lives;

        if (timerText == null)
            return;

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

    // ---------------- DAILY REWARD ----------------
    private void LoadDailyRewardTimer()
    {
        string nextRewardString = PlayerPrefs.GetString("NextRewardTime", "");
        if (!string.IsNullOrEmpty(nextRewardString))
            nextRewardTime = System.DateTime.Parse(nextRewardString);
        else
            nextRewardTime = System.DateTime.Now;

        UpdateDailyRewardButtonState();
    }

    private void UpdateDailyRewardButtonState()
    {
        if (System.DateTime.Now >= nextRewardTime)
        {
            rewardButton.interactable = true;
            if (dailyRewardTimerText != null)
                dailyRewardTimerText.text = "Reward ready!";
        }
        else
        {
            rewardButton.interactable = false;
        }
    }

    private void UpdateDailyRewardTimerUI()
    {
        if (dailyRewardTimerText == null)
            return;

        if (System.DateTime.Now < nextRewardTime)
        {
            System.TimeSpan remaining = nextRewardTime - System.DateTime.Now;
            dailyRewardTimerText.text = "Next reward in: " +
                remaining.Hours.ToString("00") + ":" +
                remaining.Minutes.ToString("00") + ":" +
                remaining.Seconds.ToString("00");
        }
        else
        {
            dailyRewardTimerText.text = "Reward ready!";
            rewardButton.interactable = true;
        }
    }

    public void OnDailyRewardButton()
    {
        if (System.DateTime.Now < nextRewardTime)
        {
            messageText.text = "Please wait for the timer.";
            return;
        }

        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += dailyRewardCoins;

        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        nextRewardTime = System.DateTime.Now.AddHours(dailyRewardCooldownHours);
        PlayerPrefs.SetString("NextRewardTime", nextRewardTime.ToString());
        PlayerPrefs.Save();

        UpdateCoinDisplay();
        messageText.text = $"You received {dailyRewardCoins} coins!";
        rewardButton.interactable = false;
    }

    // ---------------- HOURLY REWARD ----------------
    private void LoadHourlyRewardTimer()
    {
        string nextHourlyRewardString = PlayerPrefs.GetString("NextHourlyRewardTime", "");
        if (!string.IsNullOrEmpty(nextHourlyRewardString))
            nextHourlyRewardTime = System.DateTime.Parse(nextHourlyRewardString);
        else
            nextHourlyRewardTime = System.DateTime.Now;

        UpdateHourlyRewardButtonState();
    }

    private void UpdateHourlyRewardButtonState()
    {
        if (System.DateTime.Now >= nextHourlyRewardTime)
        {
            hourlyRewardButton.interactable = true;
            if (hourlyRewardTimerText != null)
                hourlyRewardTimerText.text = "Hourly reward ready!";
        }
        else
        {
            hourlyRewardButton.interactable = false;
        }
    }

    private void UpdateHourlyRewardTimerUI()
    {
        if (hourlyRewardTimerText == null)
            return;

        if (System.DateTime.Now < nextHourlyRewardTime)
        {
            System.TimeSpan remaining = nextHourlyRewardTime - System.DateTime.Now;
            hourlyRewardTimerText.text = "Next hourly reward in: " +
                remaining.Minutes.ToString("00") + ":" +
                remaining.Seconds.ToString("00");
        }
        else
        {
            hourlyRewardTimerText.text = "Hourly reward ready!";
            hourlyRewardButton.interactable = true;
        }
    }

    public void OnHourlyRewardButton()
    {
        if (System.DateTime.Now < nextHourlyRewardTime)
        {
            messageText.text = "Please wait for the hourly timer.";
            return;
        }

        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += hourlyRewardCoins;

        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        nextHourlyRewardTime = System.DateTime.Now.AddHours(hourlyRewardCooldownHours);
        PlayerPrefs.SetString("NextHourlyRewardTime", nextHourlyRewardTime.ToString());
        PlayerPrefs.Save();

        UpdateCoinDisplay();
        messageText.text = $"You received {hourlyRewardCoins} coins!";
        hourlyRewardButton.interactable = false;
    }
}
