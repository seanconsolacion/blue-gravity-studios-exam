using Camera;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    [SerializeField] private GameObject _playerPrefab;

    public PlayerLogic CurrentPlayer {  get; private set; }

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        CurrentPlayer = Instantiate(_playerPrefab).GetComponent<PlayerLogic>();
        CameraManager.Singleton.SetCameraFollow(CurrentPlayer.transform);
    }

    public void TogglePlayerController(bool isActivated)
    {
        CurrentPlayer.PlayerController.ToggleActivation(isActivated);
    }
}
