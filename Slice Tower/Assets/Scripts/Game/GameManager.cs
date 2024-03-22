using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action OnAddedScore;

    [SerializeField] private PlateSpawner[] _spawners; 

    [SerializeField] private CameraController _camera;

    private PlateSpawner _currentSpawner;
 
    private int _spawnerIndex = 0;

    private bool _isFirsTap = true;

    private bool _isGameOver = false;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        MovingPlate.OnGameOwer += GameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (MovingPlate.CurrentPlate != null && _isFirsTap == false)
            {
                if (MovingPlate.CurrentPlate.Stop())
                {
                    AddScore();
                    _camera.LiftCamera();
                    Spawn();
                }
            }
            else
            {
                StartGame();
                _isFirsTap = false;
            }
        }
    }

    private void AddScore()
    {
        if (MovingPlate.CurrentPlate.Stop())
        {
            GameManager.OnAddedScore?.Invoke();
        }
    }

    private void Spawn()
    {
        if (_isGameOver == false)
        {

            _spawnerIndex = _spawnerIndex == 0 ? 1 : 0;
            _currentSpawner = _spawners[_spawnerIndex];

            _currentSpawner.SpawnPlate();
        }
    }
    public void StartGame()
    {
        Spawn();
    }

    private void GameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    IEnumerator GameOverDelay()
    {
        _isGameOver = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        MovingPlate.OnGameOwer -= GameOver;
    }
}