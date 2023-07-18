using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class MagnetPowerup : MonoBehaviour
{
    [SerializeField]
    private GameObject _magnetGameObject;
    [SerializeField]
    private float _duration = 5.0f;

    private CarController _carController;

    private void Awake()
    {
        _carController = GetComponent<CarController>();

        _magnetGameObject.SetActive(_carController.coinMagnet);
    }

    public void Activate()
    {
        _magnetGameObject.SetActive(true);
        _carController.coinMagnet = true;

        StartCoroutine(Deactivate());
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(_duration);

        _magnetGameObject.SetActive(false);
        _carController.coinMagnet = false;
    }
}
