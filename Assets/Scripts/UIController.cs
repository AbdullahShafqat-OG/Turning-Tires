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

    [field: SerializeField]
    public GameObject postStatePanel { get; private set; }
    public Button replayBtn;

    public void Open(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void Close(GameObject panel)
    {
        panel.SetActive(false);
    }
}
