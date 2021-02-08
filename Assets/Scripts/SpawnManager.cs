using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShip = null;
    [SerializeField]
    private GameObject[] _powerUps = null;
    private int powerUpID;
    // Start is called before the first frame update
    void Start()
    {
       
        StartCoroutine(EnemySpawnCoroutine(5f));
        StartCoroutine(PowerUpsSpawnCoroutine(20f, 0));
        StartCoroutine(PowerUpsSpawnCoroutine(14f, 1));
        StartCoroutine(PowerUpsSpawnCoroutine(10f, 2));
    }

    
    private IEnumerator EnemySpawnCoroutine(float seconds)
    {
        while(true)
        {
            yield return new WaitForSeconds(seconds);
            Instantiate(_enemyShip, new Vector3(200f, 200f, 200f),Quaternion.identity);
        }
        
    }
    private IEnumerator PowerUpsSpawnCoroutine(float seconds, int powerUpID)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            Instantiate(_powerUps[powerUpID], new Vector3(200f, 200f, 200f), Quaternion.identity);
            
        }

    }
   
}
