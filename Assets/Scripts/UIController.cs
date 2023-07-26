using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Pre State")]
    public Button playBtn;
    [field: SerializeField]
    public GameObject preStatePanel { get; private set; }

    [Header("Play State")]
    public TMP_Text scoreTxt;
    [field: SerializeField]
    public GameObject playStatePanel { get; private set; }
    public TMP_Text coinsTxt;
    public TMP_Text destructionTxt;

    [Header("Post State")]
    public TMP_Text postScoreTxt;
    public TMP_Text postCoinsTxt;
    public TMP_Text postDestructionTxt;
    [field: SerializeField]
    public GameObject postStatePanel { get; private set; }

    [Header("Pause State")]
    public Button replayBtn;
    [field: SerializeField]
    public GameObject pauseStatePanel { get; private set; }


    private bool paused = false;
    public bool Paused { get => paused; private set => paused = value; }

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.UI_UPDATE_SCORE, UpdateScoreText);
        Messenger<int>.AddListener(GameEvent.UI_UPDATE_COINS, UpdateCoinsText);
        Messenger<int>.AddListener(GameEvent.UI_UPDATE_DESTRUCTION, UpdateDestructionText);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.UI_UPDATE_SCORE, UpdateScoreText);
        Messenger<int>.RemoveListener(GameEvent.UI_UPDATE_COINS, UpdateCoinsText);
        Messenger<int>.RemoveListener(GameEvent.UI_UPDATE_DESTRUCTION, UpdateDestructionText);
    }

    public void Open(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void Close(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void Pause()
    {
        Paused = !Paused;
    }

    private void UpdateScoreText(int value)
    {
        scoreTxt.text = "Score: " + value;
        postScoreTxt.text = value.ToString();
    }

    private void UpdateCoinsText(int value)
    {
        coinsTxt.text = "Coins: " + value;
        postCoinsTxt.text = "+" + value;
    }

    private void UpdateDestructionText(int value)
    {
        destructionTxt.text = "Destruction: " + value;
        postDestructionTxt.text = "+" + value;
    }
}
