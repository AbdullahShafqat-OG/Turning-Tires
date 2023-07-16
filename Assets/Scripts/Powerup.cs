using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered by player");
        string tag = this.tag;
        switch (tag)
        {
            case "Shield":
                other.GetComponent<ShieldPowerup>().Activate();
                break;
            case "Magnet":
                break;
            case "Annihilate":
                Debug.Log("Annihilating");
                other.GetComponent<AnnihilatePowerup>().Activate();
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }
}
