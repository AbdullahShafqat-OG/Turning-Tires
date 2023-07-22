using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class GhostPowerup : Powerup
{
    [SerializeField]
    private float _duration = 5.0f;

    public override void Activate()
    {
        base.Activate();

        _carController.ghost = true;
        StartCoroutine(PowerupCountdown());
    }

    private IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(_duration);

        if (_blinkCoroutine == null)
            _blinkCoroutine = StartCoroutine(BlinkCoroutine());
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        _carController.ghost = false;
    }
}
