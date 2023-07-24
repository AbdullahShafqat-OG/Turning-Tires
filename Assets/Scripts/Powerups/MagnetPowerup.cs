using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetPowerup : DurationPowerup
{
    protected override bool PowerupEffect
    {
        get { return _carController.coinMagnet; }
        set { _carController.coinMagnet = value; }
    }
}
