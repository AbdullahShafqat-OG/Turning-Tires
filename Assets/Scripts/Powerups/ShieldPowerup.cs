using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class ShieldPowerup : MonoBehaviour
{
    [SerializeField]
    private GameObject _shieldGameobject;
    [SerializeField]
    private float _duration = 2.0f;

    private CarController _carController;

    private void Awake()
    {
        _carController = GetComponent<CarController>();

        _shieldGameobject.SetActive(_carController.shield);
    }

    public void Activate()
    {
        _shieldGameobject.SetActive(true);
        _carController.shield = true;

        StartCoroutine(Deactivate());
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(_duration);

        _shieldGameobject.SetActive(false);
        _carController.shield = false;
    }
}
