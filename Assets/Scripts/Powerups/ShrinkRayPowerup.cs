using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShrinkRayPowerup : DurationPowerup
{
    [SerializeField]
    private Vector3 _targetScale;
    [SerializeField]
    private float _targetPositionY;

    private Vector3 _initialScale;
    private Vector3 _initialPosition;

    [SerializeField]
    private bool _powerupEffect = false;

    [SerializeField]
    private float _scaleDuration = 0.2f;

    protected override bool PowerupEffect
    {
        get { return _powerupEffect; }
        set { _powerupEffect = value; }
    }

    protected override void Awake()
    {
        base.Awake();

        _initialScale = _carController.transform.localScale;
        _initialPosition = _carController.transform.position;
    }

    public override void Activate()
    {
        base.Activate();

        Vector3 targetPosition = 
            new Vector3(_carController.transform.position.x, _targetPositionY, _carController.transform.position.z);
        //_carController.transform.SetPositionAndScale(targetPosition, _targetScale);

        _carController.transform.DOMoveY(_targetPositionY, _scaleDuration);
        _carController.transform.DOScale(_targetScale, _scaleDuration);
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        _initialPosition.x = _carController.transform.position.x;
        _initialPosition.z = _carController.transform.position.z;
        if (_carController.ghost)
        {
            //_carController.transform.localScale = _initialScale;
            _carController.transform.DOScale(_initialScale, _scaleDuration);
        }
        else
        {
            //_carController.transform.SetPositionAndScale(_initialPosition, _initialScale);
            _carController.transform.DOMoveY(_initialPosition.y, _scaleDuration);
            _carController.transform.DOScale(_initialScale, _scaleDuration);
        }
    }
}
