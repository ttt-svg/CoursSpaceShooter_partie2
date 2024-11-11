using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GestionScene : MonoBehaviour
{

    [SerializeField] private GameObject _startButton = default;
    [SerializeField] private GameObject _scorePanel = default;
    [SerializeField] private GameObject _boutonRetour = default;
    [SerializeField] private GameObject _boutonDemarrer = default;
    [SerializeField] private GameObject _boutonScore = default;
    [SerializeField] private TMP_Text _txtCompteur = default;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        int compteur = PlayerPrefs.GetInt("compteur");
        _txtCompteur.text = "Nombre de parties : " + compteur.ToString();
        // Sélectionne le bouton démarrer au chargement de la scène
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_startButton);
    }
    public void Quitter()
    {
        Application.Quit();
    }

    public void ChangerScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public void ApparitionScore()
    {
        _scorePanel.SetActive(true);
        _boutonRetour.SetActive(true);
        _boutonDemarrer.SetActive(false);
        _boutonScore.SetActive(false);
        // Sélectionne le bouton de retour
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_boutonRetour);
        StartCoroutine(DelaiRetour());

    }

    IEnumerator DelaiRetour()
    {
        yield return new WaitForSeconds(30.0f);
        RetourMenu();
    }

    public void RetourMenu()
    {
        _scorePanel.SetActive(false);
        _boutonRetour.SetActive(false);
        _boutonDemarrer.SetActive(true);
        _boutonScore.SetActive(true);
        // Sélectionne le bouton de retour
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_boutonDemarrer);
    }
}
