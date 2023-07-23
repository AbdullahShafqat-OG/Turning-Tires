using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnihilatePowerup : DurationPowerup
{
    protected override bool PowerupEffect
    {
        get { return _carController.annihilator; }
        set { _carController.annihilator = value; }
    }
}
