using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 1f;
    public int lifeBar = 50;
    public int lives = 1;
    public string lastCollide;

    [SerializeField]
    private GameObject _EnemyExplosion = null;
    private UIManager _uIManager = null;
    private Canvas _canvas = null;

    private float _topBound = 5.6f;
    private float _bottomBound = -5.63f;
    private float _rightRangeBound = 8.56f;
    private float _leftRangeBound = -8.56f;
    //private float _rightBound = 9.45f;
    //private float _leftBound = -9.45f;

    // Start is called before the first frame update
    void Start()
    {
        UIConfiguration();
        transform.position = new Vector3(Random.Range(_leftRangeBound, _rightRangeBound), _topBound);
    }
    private void UIConfiguration()
    {
        _canvas = GameObject.FindObjectOfType<Canvas>();
        if (_canvas != null)
        {
            _uIManager = _canvas.GetComponent<UIManager>();
        }
    }

    // Update is called once per frame
    void Update()
    { 
        Movement();
    }

    //Life mechanics
    public void LifeCalculations()
    {
        if (lifeBar < 1)
        {
            lives --;
            lifeBar = 100;
        }
        if (lives < 1)
        {
            lifeBar = 0;
            Debug.Log("Destroyed by: " + lastCollide);
            if (_uIManager != null)
            {
                _uIManager.ScoreUpdate(100);
            }
            Instantiate(_EnemyExplosion,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
    //Movement mechanics
    public void Movement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y <= _bottomBound)
        {
            transform.position = new Vector3(Random.Range(_leftRangeBound, _rightRangeBound), _topBound, transform.position.z);
        }
    }
    //Collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        lastCollide = other.tag;
        switch(other.tag)
        {
            case "Laser":
                {
                    Laser laser = other.GetComponent<Laser>();
                    if (laser != null)
                    {
                        lifeBar -= 25;
                        laser.Colission();                       
                    }
                    break;
                }
            case "Player":
                {
                    Player player = other.GetComponent<Player>();
                    if (player != null)
                    {
                        lives--;
                        player.Damage(1);                       
                    }
                    break;
                }
            case "Shield":
                {
                    Player player = other.GetComponentInParent<Player>();
                    if (player != null)
                    {
                        lives--;
                        player.Damage(1);                          
                    }
                    break;
                }
        }
        LifeCalculations();
        StartCoroutine(Waiting(0.2f));
    }

    private IEnumerator Waiting(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
