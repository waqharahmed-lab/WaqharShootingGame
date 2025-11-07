using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // ✅ Function called when Play button is clicked
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);  // make sure name matches your scene file
    }
}