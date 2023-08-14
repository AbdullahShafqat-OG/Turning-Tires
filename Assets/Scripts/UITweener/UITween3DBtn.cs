using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class UITween3DBtn : MonoBehaviour
{
    [Header("Tween Parameters")]
    [SerializeField]
    private float _deltaY = 0.5f;
    [SerializeField]
    private float _deltaScale = 0.1f;
    [SerializeField]
    private float _duration = 0.5f;
    [SerializeField]
    private Ease _ease = Ease.OutElastic;

    [SerializeField]
    private Transform _knobTransform;

    private float _initialY;
    private Vector3 _initialScale;
    private Vector3 _pressedScale;

    private void Awake()
    {
        if (_knobTransform == null)
        {
            _knobTransform = transform.GetChild(0);
        }
    }

    private void Start()
    {
        _initialY = _knobTransform.position.y;
        _deltaY = _initialY - _deltaY;

        _initialScale = transform.localScale;
        _pressedScale = new Vector3(
            _initialScale.x + _deltaScale, 
            _initialScale.y - _deltaScale, 
            _initialScale.z + _deltaScale);
    }

    private void OnMouseDown()
    {
        _knobTransform.DOMoveY(_deltaY, _duration).SetEase(_ease);
        transform.DOScale(_pressedScale, _duration).SetEase(_ease);
    }

    private void OnMouseUp()
    {
        _knobTransform.DOMoveY(_initialY, _duration).SetEase(_ease);
        transform.DOScale(_initialScale, _duration).SetEase(_ease);
    }
}
