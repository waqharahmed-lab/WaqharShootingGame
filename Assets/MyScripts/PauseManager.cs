using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;  // ✅ Add this

public class PauseManager : MonoBehaviour
{
    public Button pauseButton;
    private bool isPaused = false;
    private TextMeshProUGUI buttonText;

    void Start()
    {
        if (pauseButton == null)
        {
            Debug.LogError("Pause Button not assigned!");
            return;
        }

        // ✅ Find the TMP text component once
        buttonText = pauseButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText == null)
            Debug.LogError("No TextMeshProUGUI found inside the Pause Button!");

        // ✅ Add click listener
        pauseButton.onClick.AddListener(TogglePause);
    }

    void TogglePause()
    {
        // ✅ Deselect any UI object so spacebar won’t re-trigger it
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);

        if (buttonText == null)
        {
            Debug.LogError("Pause button text not found!");
            return;
        }

        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}
