using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserSpeed = 11.0f;
    public float laserYDestroyBound = 5.6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); 
    }
    public void Colission()
    {
        Object.Destroy(gameObject);
    }
    private void Movement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed);
        if (transform.position.y >= laserYDestroyBound)
        {
            Object.Destroy(gameObject);
        }
    }
}
