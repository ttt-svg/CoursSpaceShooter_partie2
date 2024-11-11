using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _points = 100;
    [SerializeField] private GameObject _enemyLaserPrefab = default;
    [SerializeField] private GameObject _explosionPrefab = default;
    [SerializeField] private AudioClip _sonExplosion = default;

    private UIManager _uiManager;
    private float _fireRate;
    private float _canFire;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        _canFire = Random.Range(0.5f, 1f);
    }

    void Update()
    {
        //Déplace l'ennemi vers le bas et s'il sort de l'écran le replace en
        //haut de la scène à une position aléatoire en X
        DeplacementEnnemi();

        //Méthode qui gère le tir de laser par les ennemis
        TirEnnemi();
        
    }

    private void TirEnnemi()
    {
        if (_uiManager.getScore() > 500)
        {
            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(3f, 10f);
                _canFire = Time.time + _fireRate;
                Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0f, -0.9f, 0f), Quaternion.identity);
            }
        }
    }

    private void DeplacementEnnemi()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _uiManager.getVitesseEnnemi());
        if (transform.position.y <= -5f)
        {
            float randomX = Random.Range(-8.17f, 8.17f);
            transform.position = new Vector3(randomX, 8f, 0f);
        }
    }

    // Gère les collisions entre les ennemis et les lasers/joueur
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la collision survient avec le joueur
        if (other.tag == "Player")
        {
            //Récupérer la classe Player afin d'accéder aux méthodes publiques
            Player player = other.transform.GetComponent<Player>();
            player.Degats();  // Appeler la méthode dégats du joueur
            
            if (transform.childCount > 0)
            {
                GameObject _shield = transform.GetChild(0).gameObject;
                Destroy(_shield);
                AudioSource.PlayClipAtPoint(_sonExplosion, Camera.main.transform.position, 0.5f);
            }
            else
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject); // Détruire l'objet ennemi
            }
            
        }
        // Si la collision se produit avec un laser
        else if(other.tag == "Laser")
        {
            // Détruit l'ennemi et le laser
            
            Destroy(other.gameObject);
            if (transform.childCount > 0)
            {
                GameObject _shield = transform.GetChild(0).gameObject;
                Destroy(_shield);
            }
            else
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject); // Détruire l'objet ennemi
                _uiManager.AjouterScore(_points); // Appelle la méthode dans la classe UIManger pour augmenter le pointage
            }
        }
    }
}
