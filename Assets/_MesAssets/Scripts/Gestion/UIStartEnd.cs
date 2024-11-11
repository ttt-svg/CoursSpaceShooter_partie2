using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class UIStartEnd : MonoBehaviour
{
    private void Start()
    {

    }
    public void OnQuitterClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnDemarrerClick()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}
