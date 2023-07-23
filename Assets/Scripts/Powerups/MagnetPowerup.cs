using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetPowerup : DurationPowerup
{
    //[SerializeField]
    //private float _duration = 5.0f;
    //[SerializeField]
    //private Slider _slider;

    //private float _currentTime;
    //private bool _isCounting = false;

    protected override bool PowerupEffect
    {
        get { return _carController.coinMagnet; }
        set { _carController.coinMagnet = value; }
    }

    //public override void Activate()
    //{
    //    _slider.maxValue = _duration + _bufferTime;
    //    _currentTime = _slider.maxValue;
    //    _slider.value = _slider.maxValue;
    //    _isCounting = true;

    //    if (PowerupEffect)
    //    {
    //        Debug.Log("Already MAGNET");
    //        StopAllCoroutines();
    //    }

    //    base.Activate();

    //    _slider.gameObject.SetActive(PowerupEffect);


    //    StartCoroutine(PowerupCountdown());
    //}

    //private void Update()
    //{
    //    if (_isCounting)
    //    {
    //        _currentTime -= Time.deltaTime;
    //        if (_currentTime <= 0f)
    //        {
    //            _currentTime = 0f;
    //            _isCounting = false;
    //            // Call any function you want here when the countdown ends
    //        }
    //        _slider.value = _currentTime;
    //    }
    //}

    //private IEnumerator PowerupCountdown()
    //{
    //    yield return new WaitForSeconds(_duration);

    //    if (_blinkCoroutine == null)
    //        _blinkCoroutine = StartCoroutine(BlinkCoroutine());
    //}

    //protected override void Deactivate()
    //{
    //    base.Deactivate();

    //    _slider.gameObject.SetActive(PowerupEffect);
    //}
}
