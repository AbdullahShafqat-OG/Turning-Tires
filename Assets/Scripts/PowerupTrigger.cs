using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTrigger : MonoBehaviour
{
    private AudioClip _powerupCollectSFX;

    private void Awake()
    {
        _powerupCollectSFX = Resources.Load<AudioClip>("Music/pickup_1");    
    }

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
                other.GetComponent<JetpackPowerup>().Activate();
                break;
            case "ShrinkRay":
                other.GetComponent<ShrinkRayPowerup>().Activate();
                break;
            default:
                break;
        }

        other.GetComponent<AudioSource>().PlayOneShot(_powerupCollectSFX);

        Destroy(gameObject);
    }
}
