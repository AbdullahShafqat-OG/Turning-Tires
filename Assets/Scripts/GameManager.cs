using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Spawner spawner;
    public ObstacleSpawner obstacleSpawner;
    public DecorationSpawner decorationSpawner;
    public CoinSpawner coinSpawner;
    public CarController carController;
    public UIController uiController;

    [Header("Game Parameters")]
    public float speedDifficultyRamp = 1.0f;
    public float speedDifficultyRampTime = 5.0f;

    public float difficultyModeSwitch = 36.0f;

    public float obstacleDifficultyRamp = 1.0f;
    public float obstacleDifficultyRampTime = 5.0f;

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
