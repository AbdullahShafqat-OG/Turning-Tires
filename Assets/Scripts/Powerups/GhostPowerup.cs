using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class GhostPowerup : MonoBehaviour
{
    [SerializeField]
    private GameObject _ghostGameObject;
    [SerializeField]
    private float _duration = 5.0f;

    private CarController _carController;

    private void Awake()
    {
        _carController = GetComponent<CarController>();
        _ghostGameObject.SetActive(_carController.ghost);
    }

    public void Activate()
    {
        _ghostGameObject.SetActive(true);
        _carController.ghost = true;

        StartCoroutine(Deactivate());
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(_duration);

        _ghostGameObject.SetActive(false);
        _carController.ghost = false;
    }
}
