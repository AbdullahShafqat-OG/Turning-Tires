using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup
{
    protected override bool PowerupEffect
    {
        get { return _carController.shield; }
        set { _carController.shield = value; }
    }

    private bool _contactingObstacle = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _contactingObstacle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_contactingObstacle && _blinkCoroutine == null)
        {
            _blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }
    }
}
