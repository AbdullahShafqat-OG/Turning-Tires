using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [field: SerializeField]
    public int Value { get; private set; }

    [SerializeField] private float moveDistance = 2.0f;
    [SerializeField] private float moveDuration = 1.0f;

    [SerializeField] private Vector3 rotationAmount = new Vector3(0, 360, 0);
    [SerializeField] private float rotationDuration = 2.0f;
    [SerializeField] private Ease rotationEase;

    private void Start()
    {
        Vector3 endPos = transform.position + new Vector3(0, moveDistance, 0);
        transform.DOMoveY(endPos.y, moveDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(rotationAmount, rotationDuration, RotateMode.Fast).SetEase(rotationEase).SetLoops(-1, LoopType.Incremental);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
