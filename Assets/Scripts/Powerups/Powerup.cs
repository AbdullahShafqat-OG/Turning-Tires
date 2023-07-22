using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    protected GameObject _visualGameobject;

    [SerializeField]
    private Material _blinkMaterial;
    [SerializeField]
    private float _bufferTime = 1.0f;
    [SerializeField]
    private int _blinkFrequency = 1;

    protected CarController _carController;

    private MeshRenderer _meshRenderer;
    private Material _defaultMaterial;

    protected Coroutine _blinkCoroutine = null;

    private void Awake()
    {
        _meshRenderer = _visualGameobject.GetComponent<MeshRenderer>();
        _defaultMaterial = _meshRenderer.material;

        _carController = GetComponent<CarController>();

        _visualGameobject.SetActive(false);
    }

    public virtual void Activate()
    {
        _visualGameobject.SetActive(true);

        _meshRenderer.material = _defaultMaterial;
    }

    protected virtual void Deactivate()
    {
        Debug.Log("IN teh PARENT deactivate");


        _visualGameobject.SetActive(false);

        _blinkCoroutine = null;
    }

    protected IEnumerator BlinkCoroutine()
    {
        float elapsedTime = 0;
        float frameCount = 0;

        while (elapsedTime <= _bufferTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            frameCount++;

            var blink = Mathf.PingPong(frameCount * Time.timeScale, 10 / _blinkFrequency);

            if (blink == 10 / _blinkFrequency)
                ToggleMaterial();

            if (elapsedTime >= _bufferTime)
                Deactivate();
        }
    }

    private void ToggleMaterial()
    {
        if (_blinkMaterial != null)
        {
            _meshRenderer.material = _meshRenderer.material == _defaultMaterial ? _blinkMaterial : _defaultMaterial;
        }
    }
}
