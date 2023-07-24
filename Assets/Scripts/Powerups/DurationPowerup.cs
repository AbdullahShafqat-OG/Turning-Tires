using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DurationPowerup : Powerup
{
    [SerializeField]
    private float _duration = 5.0f;
    [SerializeField]
    private GameObject _sliderPrefab;
    [SerializeField]
    private Transform _parent;

    protected TMP_Text _sliderTxt;

    private Slider _durationSlider;
    private float _currentTime;
    private bool _isCounting = false;

    private void Start()
    {
        GameObject go = Instantiate(_sliderPrefab);
        go.transform.SetParent(_parent, false);
        _durationSlider = go.GetComponent<Slider>();
        _durationSlider.maxValue = _duration + _bufferTime;
        _durationSlider.gameObject.SetActive(PowerupEffect);

        _sliderTxt = go.GetComponentInChildren<TMP_Text>();
        _sliderTxt.text = this.GetType().Name;
    }

    public override void Activate()
    {
        _currentTime = _durationSlider.maxValue;
        _durationSlider.value = _durationSlider.maxValue;
        _isCounting = true;

        if (PowerupEffect)
        {
            StopAllCoroutines();
        }

        base.Activate();

        _durationSlider.gameObject.SetActive(PowerupEffect);

        StartCoroutine(PowerupCountdown());
    }

    private IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(_duration);

        if (_blinkCoroutine == null)
            _blinkCoroutine = StartCoroutine(BlinkCoroutine());
    }

    private void Update()
    {
        if (_isCounting)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0f)
            {
                _currentTime = 0f;
                _isCounting = false;
                // Call any function you want here when the countdown ends
            }
            _durationSlider.value = _currentTime;
        }
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        _durationSlider.gameObject.SetActive(PowerupEffect);
    }
}
