using UnityEngine;
using TMPro;
using System.Collections;

public class KillBonusManager : MonoBehaviour
{
    
    public int requiredKills = 10;
    public int bonusCoins = 250;
    public float winMessageDuration = 3f;

   
    public TextMeshProUGUI winText;
    public GameManager gameManager; // Reference to your main GameManager

    private int enemiesKilled = 0;
    private bool bonusGiven = false;

    // Call this when a zombie is killed
    public void EnemyKilled()
    {
        if (bonusGiven) return;

        enemiesKilled++;

        if (enemiesKilled >= requiredKills)
        {
            GiveBonus();
        }
    }

    private void GiveBonus()
    {
        bonusGiven = true;

        if (gameManager != null)
        {
            gameManager.AddCoins(bonusCoins);
        }

        if (winText != null)
        {
            winText.gameObject.SetActive(true);
            winText.text = "You Win! Bonus: " + bonusCoins + " Coins";
            StartCoroutine(FadeOutWinText());
        }

        Debug.Log("🎉 Player killed " + requiredKills + " enemies! Bonus: " + bonusCoins + " coins.");
    }

    private IEnumerator FadeOutWinText()
    {
        float elapsed = 0f;
        TextMeshProUGUI text = winText;
        Color originalColor = text.color;

        while (elapsed < winMessageDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / winMessageDuration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        text.gameObject.SetActive(false);
        text.color = originalColor;
    }
}
