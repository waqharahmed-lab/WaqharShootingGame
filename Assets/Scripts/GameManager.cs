using UnityEngine;
using UnityEngine.SceneManagement;  // ✅ Required for scene reloading

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    private bool isGameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f; // Normal speed at start
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("GAME OVER!");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Pause everything
        }
    }

  
    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        Time.timeScale = 1f; // Resume time before reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }
}
