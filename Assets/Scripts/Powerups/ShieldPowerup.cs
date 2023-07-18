using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class ShieldPowerup : MonoBehaviour
{
    [SerializeField]
    private GameObject _shieldGameobject;

    private CarController _carController;

    private bool _contactingObstacle = false;

    private void Awake()
    {
        _carController = GetComponent<CarController>();

        _shieldGameobject.SetActive(_carController.shield);
    }

    public void Activate()
    {
        _shieldGameobject.SetActive(true);
        _carController.shield = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _contactingObstacle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_contactingObstacle)
        {
            _shieldGameobject.SetActive(false);
            _carController.shield = false;
        }
    }
}
