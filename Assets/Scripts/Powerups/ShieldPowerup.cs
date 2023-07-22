using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class ShieldPowerup : MonoBehaviour
{
    [SerializeField]
    private GameObject _shieldGameobject;

    [SerializeField]
    private Material _blinkMaterial;
    [SerializeField]
    private float _bufferTime = 1.0f;
    [SerializeField]
    private int _blinkFrequency = 1;

    private CarController _carController;

    private bool _contactingObstacle = false;
    private MeshRenderer _meshRenderer;
    private Material _defaultMaterial;

    private Coroutine _deactivate = null;

    private void Awake()
    {
        _meshRenderer = _shieldGameobject.GetComponent<MeshRenderer>();
        _defaultMaterial = _meshRenderer.material;

        _carController = GetComponent<CarController>();

        _shieldGameobject.SetActive(_carController.shield);
    }

    public void Activate()
    {
        _shieldGameobject.SetActive(true);
        _carController.shield = true;

        _meshRenderer.material = _defaultMaterial;
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
        if (_contactingObstacle && _deactivate == null)
        {
            _deactivate = StartCoroutine(DetonateCoroutine());
        }
    }

    private void Deactivate()
    {
        _shieldGameobject.SetActive(false);
        _carController.shield = false;

        _deactivate = null;
    }

    private IEnumerator DetonateCoroutine()
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
