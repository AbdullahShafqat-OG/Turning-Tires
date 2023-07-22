using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string tag = this.tag;
        switch (tag)
        {
            case "Shield":
                other.GetComponent<ShieldPowerup>().Activate();
                break;
            case "Magnet":
                other.GetComponent<MagnetPowerup>().Activate();
                break;
            case "Annihilate":
                other.GetComponent<AnnihilatePowerup>().Activate();
                break;
            case "Ghost":
                other.GetComponent<GhostPowerup>().Activate();
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }
}
