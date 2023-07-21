using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITweenTitle : MonoBehaviour
{
    [System.Serializable]
    public struct UITweenTitleData
    {
        public GameObject txt;
        public RectTransform endRect;
        public float duration;
        public Ease ease;
    }

    public UITweenTitleData[] titleDatas;

    private void Start()
    {
        foreach (UITweenTitleData data in titleDatas)
        {
            Vector2 startPos = data.endRect.anchoredPosition;

            data.txt.GetComponent<RectTransform>().DOAnchorPos(startPos, data.duration).SetEase(data.ease);
        }
    }
}
