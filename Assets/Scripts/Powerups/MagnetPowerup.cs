using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class MagnetPowerup : Powerup
{
    //[SerializeField]
    //private GameObject _magnetGameObject;
    [SerializeField]
    private float _duration = 5.0f;

    //private CarController _carController;

    //private void Awake()
    //{
    //    _carController = GetComponent<CarController>();

    //    _magnetGameObject.SetActive(_carController.coinMagnet);
    //}

    public override void Activate()
    {
        base.Activate();

        //_magnetGameObject.SetActive(true);
        _carController.coinMagnet = true;

        StartCoroutine(PowerupCountdown());
    }

    private IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(_duration);

        //_magnetGameObject.SetActive(false);
        _carController.coinMagnet = false;
        if (_blinkCoroutine == null)
            _blinkCoroutine = StartCoroutine(BlinkCoroutine());
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        Debug.Log("IN teh child deactivate");
        _carController.coinMagnet = false;
    }
}
