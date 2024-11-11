
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private float _vitesseEnnemi = 6.0f;
    public float VitesseEnnemi => _vitesseEnnemi;

    private int _score;
    public int Score => _score;

    private void Start()
    {
        _score = 0;
        
    }

    private void Update()
    {

    }

    // Méthode qui permet l'augmentation du score
    public void AjouterScore(int points)
    {
        _score += points;
        UIManagerGame.Instance.UpdateScore(_score);
    }
}
