using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    [SerializeField]
    private GameObject _player = null;
    [SerializeField]
    private GameObject _spawnManager = null;
    private GameObject _actualSpawnManager = null;
    private UIManager _uIManager = null;
    private Canvas _canvas = null;

    void Start()
    {
        UIConfiguration();
    }
    private void UIConfiguration()
    {
        _canvas = GameObject.FindObjectOfType<Canvas>();
        if (_canvas != null)
        {
            _uIManager = _canvas.GetComponent<UIManager>();
            if (_uIManager != null)
            {
                _uIManager.SetMenuScreenActive(true);
            }
        }
    }
    void Update()
    {
        if(gameOver == true && Input.GetKeyDown(KeyCode.Space))
        {
            GameStart();
        }
    }

    public void GameStart()
    {
        Instantiate(_player, Vector3.zero, Quaternion.identity);
        _actualSpawnManager = Instantiate(_spawnManager, Vector3.zero, Quaternion.identity);
        gameOver = false;
        _uIManager.SetMenuScreenActive(false);
    }

    public void GameEnd()
    {
        _uIManager.SetMenuScreenActive(true);
        _uIManager.score = 0;
        Destroy(_actualSpawnManager);
        gameOver = true;
    }
}
