using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITween3DTitle : MonoBehaviour
{
    public float finalY;
    public float duration;
    public Ease ease;

    private float y;

    private void Start()
    {
        y = transform.position.y;

        transform.DOMoveY(finalY, duration).SetEase(ease);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            (finalY, y) = (y, finalY);
            transform.DOMoveY(finalY, duration).SetEase(ease);
        }
    }
}
