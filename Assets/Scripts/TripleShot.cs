﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    public float laserSpeed = 11.0f;
    public float tripleShotYDestroyBound = 5.45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed);
        if (transform.position.y >= tripleShotYDestroyBound)
        {
            Object.Destroy(gameObject);
        }
    }
}
