using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.5f;
    public int powerID = 0;
    /*
     0 = Triple Shot PowerUp
     1 = Speed Boost PowerUp
     2 = Shields PowerUp
    */
    public float _topBound = 5.38f;
    public float _bottomBound = -5.46f;
    public float _rightRangeBound = 8.77f;
    public float _leftRangeBound = -8.77f;

    void Start()
    {
        transform.position = new Vector3(Random.Range(_leftRangeBound, _rightRangeBound), _topBound);
        speed = Random.Range(0.5f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y <= _bottomBound)
        {
            Object.Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player!=null)
            {
                player.PowerUpON(powerID);
            }
            Destroy(this.gameObject);
        }
    }
}