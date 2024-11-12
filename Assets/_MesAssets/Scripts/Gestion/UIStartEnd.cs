using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIStartEnd : MonoBehaviour
{
    // Scene de fin
    [Header("Variables pour fin de partie")]
    [SerializeField] private TextMeshProUGUI _txtGameOver = default;
    [SerializeField] private TextMeshProUGUI _txtScoreFin = default;
    [SerializeField] private Button _buttonMenu = default;
    [SerializeField] private Button _buttonQuitter = default;

    [Header("Variables pour depart de partie")]
    [SerializeField] private GameObject _buttonDemarrer = default;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex==0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_buttonDemarrer);
        }



        if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            _buttonMenu.onClick.AddListener(OnMenuClick);
            _buttonQuitter.onClick.AddListener(OnQuitterClick);
            _txtScoreFin.text = "Votre pointage : " + GameManager.Instance.Score.ToString();
            GameOverSequence();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_buttonMenu.gameObject);
        }
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

    public void OnMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    // M�thode qui affiche la fin de la partie et lance la coroutine d'animation
    private void GameOverSequence()
    {
        _txtGameOver.gameObject.SetActive(true);
        StartCoroutine(GameOverBlinkRoutine());
    }

    IEnumerator GameOverBlinkRoutine()
    {
        while (true)
        {
            _txtGameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _txtGameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
