using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private float scoreWithTimeDelay = 0.1f;
    [SerializeField, Tooltip("In milliseconds")]
    private float scoreWithTimeRate = 1.0f;

    private int score;
    private int coins;

    private void Awake()
    {
        score = 0;
        coins = 0;

        Messenger.AddListener(GameEvent.COIN_COLLECTED, UpdateScoreCoin);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.COIN_COLLECTED, UpdateScoreCoin);
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

    private void UpdateScoreCoin()
    {
        coins += 10;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 20), new GUIContent(score.ToString()));
        GUI.Box(new Rect(10, 40, 100, 20), new GUIContent(coins.ToString()));
    }
}
