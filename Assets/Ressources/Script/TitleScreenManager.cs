using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("StartGame function called");
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame function called");
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
