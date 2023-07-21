using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [field: SerializeField]
    public GameObject preStatePanel { get; private set; }
    public Button playBtn;

    [field: SerializeField]
    public GameObject playStatePanel { get; private set; }
    public TMP_Text scoreTxt;
    public TMP_Text coinsTxt;
    public TMP_Text destructionTxt;

    [field: SerializeField]
    public GameObject postStatePanel { get; private set; }
    [field: SerializeField]
    public GameObject pauseStatePanel { get; private set; }

    public Button replayBtn;

    private bool paused = false;
    public bool Paused { get => paused; private set => paused = value; }

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
}
