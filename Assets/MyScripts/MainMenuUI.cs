using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // ✅ Needed for TextMeshProUGUI

public class MainMenuUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;  

    void Start()
    {
        UpdateCoinDisplay();  
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);  
    }


    private void UpdateCoinDisplay()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); 
        if (coinText != null)
        {
            coinText.text = totalCoins.ToString();  
        }
    }
}
