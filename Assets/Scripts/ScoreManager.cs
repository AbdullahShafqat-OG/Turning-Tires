using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private float scoreWithTimeDelay = 0.1f;
    [SerializeField, Tooltip("In milliseconds")]
    private float scoreWithTimeRate = 1.0f;

    public int score { get; private set; }
    public int coins { get; private set; }
    public int destruction { get; private set; }

    private void Awake()
    {
        score = 0;
        coins = 0;
        destruction = 0;

        Messenger<int>.AddListener(GameEvent.COIN_COLLECTED, UpdateScoreCoin);
        Messenger<int>.AddListener(GameEvent.OBSTACLE_DESTROYED, UpdateScoreDestruction);
        //Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.COIN_COLLECTED, UpdateScoreCoin);
        Messenger<int>.RemoveListener(GameEvent.OBSTACLE_DESTROYED, UpdateScoreDestruction);
        //Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void Start()
    {
        scoreWithTimeRate /= 1000.0f;
        InvokeRepeating("UpdateScoreTime", scoreWithTimeDelay, scoreWithTimeRate);
    }

    private void UpdateScoreTime()
    {
        score += 1;
    }

    private void UpdateScoreCoin(int value)
    {
        coins += value;
    }

    private void UpdateScoreDestruction(int value)
    {
        destruction += value;
    }

    private void OnGameOver()
    {
        CancelInvoke("UpdateScoreTime");
    }
}
