using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostPowerup : DurationPowerup
{
    [SerializeField]
    private float _targetPositionY;
    [SerializeField]
    private float _liftDuration = 0.5f;

    private Vector3 _initialPosition;

    protected override bool PowerupEffect
    {
        get { return _carController.ghost; }
        set { _carController.ghost = value; }
    }

    protected override void Awake()
    {
        base.Awake();

        _initialPosition = _carController.transform.position;
    }
    public override void Activate()
    {
        base.Activate();

        Vector3 targetPosition =
            new Vector3(_carController.transform.position.x, _targetPositionY, _carController.transform.position.z);
        //_carController.transform.position = targetPosition;
        _carController.transform.DOMoveY(_targetPositionY, _liftDuration);
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        //_initialPosition.x = _carController.transform.position.x;
        //_initialPosition.z = _carController.transform.position.z;
        //_carController.transform.position = _initialPosition;
        _carController.transform.DOMoveY(_initialPosition.y, _liftDuration);
    }

}
