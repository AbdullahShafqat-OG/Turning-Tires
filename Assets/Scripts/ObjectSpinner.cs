using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectSpinner : MonoBehaviour
{
    [SerializeField] 
    private Vector3 _rotationAmount = new Vector3(0, 360, 0);
    [SerializeField] 
    private float _rotationDuration = 2.0f;
    [SerializeField] 
    private Ease _rotationEase = Ease.Linear;

    private void Start()
    {
        transform.DORotate(_rotationAmount, _rotationDuration, RotateMode.Fast).SetEase(_rotationEase).SetLoops(-1, LoopType.Incremental);
        Debug.Log(transform.name);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
