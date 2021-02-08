using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public float fireRate = 0.25f;
    public int speedBoost = 0;
    public bool tripleShot = false;
    public bool shield = false;
    public int shieldLives = 0;
    public bool cooldown = true;
    public int lives = 3;
    public int lifeBar = 100;
    
    [SerializeField]
    private GameObject _laserPrefab = null;
    [SerializeField]
    private GameObject _tripleShotPrefab = null;
    [SerializeField]
    private GameObject _explosionAnimation = null;
    [SerializeField]
    private GameObject _shield = null;
    private GameObject _actualShield = null;
    private UIManager _uIManager = null;
    private GameManager _gameManager = null;
    private Canvas _canvas = null;

    private float _topBound = 4.27f;
    private float _bottomBound = -4.27f;
    private float _rightBound = 8.33f;
    private float _leftBound = -8.33f;
    private float _rightOffBound = 9.45f;
    private float _leftOffBound = -9.45f;
    private float _nextFire = 0.0f;
    private float _horizontalInput;
    private float _verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        
        _actualShield = Instantiate(_shield, transform.position, Quaternion.identity, transform);
        _actualShield.SetActive(false);
        UIConfiguration();
        GameManagerConfiguration();
        transform.position = new Vector3(0, 0, 0);
    }
    private void UIConfiguration()
    {
        _canvas = GameObject.FindObjectOfType<Canvas>();
        if (_canvas != null)
        {
            _uIManager = _canvas.GetComponent<UIManager>();
            if (_uIManager != null)
            {
                _uIManager.LivesUpdate(lives);
                _uIManager.ScoreUpdate(0);
            }
        }
    }
    private void GameManagerConfiguration()
    {
        {
        _gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement(1);
        LaserShoot(); 
    }
    public void PowerUpON(int powerID)
    {
        float powerTime = 5f;
        switch (powerID)
        {
            case 0://Triple Shot
                {
                    tripleShot = true;
                    powerTime = 7f;
                    StartCoroutine(PowerDown(powerID, powerTime));
                    break;
                }
            case 1://Speed Boost
                {
                    speedBoost = 1;
                    powerTime = 10f;
                    StartCoroutine(PowerDown(powerID, powerTime));
                    break;
                }
            case 2://Shields
                {
                    shield = true;
                    _actualShield.SetActive(shield);
                    shieldLives = 2;
                    break;
                }
        }
    }
    public void Damage(int damageID)
    {
        if (shieldLives > 0)
        {
            shieldLives--;
        }
        else
        {
            switch (damageID)
            {
                case 0:
                    {
                        //Laser Collision
                        lifeBar -= 25;
                        break;
                    }
                case 1:
                    {
                        //Enemy Collision
                        lives--;
                        lifeBar = 100;
                        break;
                    }
            }
        }
        LifeCalculations();
        _uIManager.LivesUpdate(lives);
        StartCoroutine(Waiting(0.2f));
    }
    private void Movement(int xMode)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.right * Time.deltaTime * (speed + (speedBoost * 3f)) * _horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * (speed + (speedBoost * 3f)) * _verticalInput);

            // Player Y Bounds
            if (transform.position.y > _topBound)
            {
                transform.position = new Vector3(transform.position.x, _topBound, 0);
            }
            else if (transform.position.y < _bottomBound)
            {
                transform.position = new Vector3(transform.position.x, _bottomBound, 0);
            }

            // Player X Bounds
            switch (xMode)
            {
                case 1:
                    {
                        xBoundsMode(1);
                        break;
                    }
                case 2:
                    {
                        xBoundsMode(2);
                        break;
                    }
                default:
                    {
                        xBoundsMode(1);
                        break;
                    }
            }
        }
    private void xBoundsMode(int mode)
    {
        switch (mode)
        {
            case 1://MODE 1 NORMAL BOUNDS
                {
                    if (transform.position.x > _rightBound)
                    {
                        transform.position = new Vector3(_rightBound, transform.position.y, 0);
                    }
                    else if (transform.position.x < _leftBound)
                    {
                        transform.position = new Vector3(_leftBound, transform.position.y, 0);
                    }
                    break;
                }
            case 2:// MODE 2 (Looping bounds)
                {
                    if (transform.position.x > _rightOffBound)
                    {
                        transform.position = new Vector3(_leftOffBound, transform.position.y, 0);
                    }
                    if (transform.position.x < _leftOffBound)
                    {
                        transform.position = new Vector3(_rightOffBound, transform.position.y, 0);
                    }
                    break;
                }
        }
    }
    private void LifeCalculations()
    {
        if(_actualShield.activeSelf==true && shieldLives < 1)
        {
            StartCoroutine(PowerDown(2,0.1f));
        }
        if (lifeBar < 1)
        {
            lives --;
            lifeBar = 100;
        }
        if (lives < 1)
        {
            lifeBar = 0;
            Instantiate(_explosionAnimation, transform.position, Quaternion.identity);
            _gameManager.GameEnd();
            Destroy(gameObject);
        }
    }
    private void LaserShoot()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            if (cooldown == true)
            {
                if (Time.time > _nextFire)
                {
                    if (tripleShot == true)
                    {
                        Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                    }
                    _nextFire = Time.time + fireRate;
                }
            }
            else
            {
                if (tripleShot == true)
                {
                    Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                }
            }
        }
    }
    private IEnumerator PowerDown(int powerID, float TSPowerDownTime)
    {
        yield return new WaitForSeconds(TSPowerDownTime);
        switch(powerID)
        {
            case 0://Triple Shot
                {
                    tripleShot = false;
                    break;
                }
            case 1://Speed Boost
                {
                    speedBoost = 0;
                    break;
                }
            case 2://Shields
                {
                    shield = false;
                    _actualShield.SetActive(shield);
                    break;
                }
        } 
    }

    private IEnumerator Waiting(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    
}