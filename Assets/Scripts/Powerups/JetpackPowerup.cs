using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JetpackPowerup : DurationPowerup
{
    [SerializeField]
    private float _targetPositionY;
    [SerializeField]
    private float _liftDuration = 0.5f;
    [SerializeField]
    private float _landDuration = 0.25f;

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

        _carController.transform.DOMoveY(_targetPositionY, _liftDuration);
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        _carController.transform.DOMoveY(_initialPosition.y, _landDuration);
    }

}
