using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private GameObject _laserPrefab = default;
    [SerializeField] private GameObject _tripleLaserPrefab = default;
    [SerializeField] private float _delai = 0.5f;
    [SerializeField] private int _viesJoueur = 3;
    
    
    private float _cadenceInitiale;
    private float _canFire = -1;
    private bool _isTripleActive = false;
    private GameObject _shield;
    private Animator _animator;

    private void Awake()
    {

    }

    void Start()
    {
        _shield = transform.GetChild(0).gameObject;
        _shield.SetActive(false);
        transform.position = new Vector3(0f, -2.4f, 0f);  // position initiale du joueur
        _cadenceInitiale = _delai;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Tir();
    }
    
    public void SpeedPowerUp()
    {
        _delai = 0.1f;
        StartCoroutine(SpeedCoroutine());
    }
    public void PowerTripleShot()
    {
        _isTripleActive= true;
        StartCoroutine(tripleCoroutine());
    }

    IEnumerator tripleCoroutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleActive = false;
    }

    IEnumerator SpeedCoroutine()
    {
        yield return new WaitForSeconds(5);
        _delai = _cadenceInitiale;
    }

    public void ShieldPowerUp()
    {
        _shield.SetActive(true);
    }

    // M�thode qui g�re le tir du joueur ainsi que le d�lai entre chaque tir
    private void Tir()
    {
        //Debug.Log(Input.GetAxis("Fire1"));
        if (Input.GetButton("Fire1") && Time.time > _canFire)
        {
            _canFire = Time.time + _delai;
            if(!_isTripleActive)
            {
                Instantiate(_laserPrefab, (transform.position + new Vector3(0f, 0.9f, 0f)), Quaternion.identity);
            }
            else
            {
                Instantiate(_tripleLaserPrefab, (transform.position + new Vector3(0f, 0.9f, 0f)), Quaternion.identity);

            }
        }
    }

    // D�placements et limitation des mouvements du joueur
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(direction * Time.deltaTime * _speed);

        if(direction.x> 0f)
        {
            _animator.SetBool("turn_right", true);
            _animator.SetBool("turn_left", false);
        }
        else if (direction.x < 0f)
        {
            _animator.SetBool("turn_left", true);
            _animator.SetBool("turn_right", false);
        }
        else
        {
            _animator.SetBool("turn_right", false);
            _animator.SetBool("turn_left", false);
        }

        //G�rer la zone verticale et horizontale
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.3f, 8.3f),
        Mathf.Clamp(transform.position.y, -3.07f, 2.3f), 0f);
    }

    // M�thodes publiques ==================================================================

    // M�thode appell� quand le joueur subit du d�gat
    public void Degats()
    {
        if(_shield.activeSelf)
        {
            _shield.SetActive(false);
        }
        else
        {
            _viesJoueur--;
            UIManagerGame.Instance.ChangeLivesDisplayImage(_viesJoueur);

        }
 
        // Si le joueur n'a plus de vie on arr�te le spwan et d�truit le joueur
        if (_viesJoueur < 1)
        {
            SpawnManager spawnManager = FindAnyObjectByType<SpawnManager>();
            spawnManager.MortJoueur();
            Destroy(this.gameObject);
        }
    }

}
