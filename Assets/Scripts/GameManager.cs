using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public ObstacleSpawner obstacleSpawner;
    public CoinSpawner coinSpawner;
    public CarController carController;
    public UIController uiController;

    private Camera cam;

    GameBaseState currentState;

    public GamePreState preState { get; private set; } = new GamePreState();
    public GamePlayState playState { get; private set; } = new GamePlayState();
    public GamePauseState pauseState { get; private set; } = new GamePauseState();
    public GamePostState postState { get; private set; } = new GamePostState();

    private void Awake()
    {
        cam = Camera.main;

        currentState = preState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    void OnGUI()
    {
        currentState.OnGUI(this);
    }
}
