using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public Button pauseButton;
    private bool isPaused = false;

    void Start()
    {
        if (pauseButton != null)
            pauseButton.onClick.AddListener(TogglePause);
        else
            Debug.LogError("Pause Button not assigned in Inspector!");
    }

    public void TogglePause() // ✅ must be public, no parameters
    {
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);

        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseButton.GetComponentInChildren<Text>().text = "Pause";
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
            pauseButton.GetComponentInChildren<Text>().text = "Play";
        }
    }
}
