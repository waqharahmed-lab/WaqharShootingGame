using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // ✅ Needed for TextMeshProUGUI

public class MainMenuUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI messageText;
    public int entryFee = 10;
    private int totalCoins;

    void Start()
    {
        UpdateCoinDisplay();
    }

    public void OnPlayButton()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        if (totalCoins >= entryFee)
        {
            totalCoins -= entryFee;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            PlayerPrefs.Save();

            Debug.Log("entry fee paid and remaining coins:" + totalCoins);
            UpdateCoinDisplay();

            SceneManager.LoadScene(1);
        }

        else
        {
            if (messageText != null)
            {
                messageText.text = " Not enough coins!";
            }
        }
    }


    private void UpdateCoinDisplay()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        if (coinText != null)
        {
            coinText.text = totalCoins.ToString();
        }
    }
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
}