using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPowerup : DurationPowerup
{
    protected override bool PowerupEffect
    {
        get { return _carController.ghost; }
        set { _carController.ghost = value; }
    }
}
