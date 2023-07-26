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

    private Coroutine _scorer = null;

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
        //InvokeRepeating("UpdateScoreTime", scoreWithTimeDelay, scoreWithTimeRate);
    }

    public bool ToggleScorer()
    {
        if (_scorer == null)
            _scorer = StartCoroutine(Scorer());
        else
        {
            StopCoroutine(_scorer);
            _scorer = null;
        }

        return _scorer != null;
    }

    private IEnumerator Scorer()
    {
        while (true)
        {
            yield return new WaitForSeconds(scoreWithTimeDelay);
            UpdateScoreTime();
        }
    }

    private void UpdateScoreTime()
    {
        score += 1;
        Messenger<int>.Broadcast(GameEvent.UI_UPDATE_SCORE, score);
    }

    private void UpdateScoreCoin(int value)
    {
        coins += value;
        Messenger<int>.Broadcast(GameEvent.UI_UPDATE_COINS, coins);
    }

    private void UpdateScoreDestruction(int value)
    {
        destruction += value;
        Messenger<int>.Broadcast(GameEvent.UI_UPDATE_DESTRUCTION, destruction);
    }

    private void OnGameOver()
    {
        CancelInvoke("UpdateScoreTime");
    }
}
