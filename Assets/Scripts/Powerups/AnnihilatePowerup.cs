using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class AnnihilatePowerup : MonoBehaviour
{
    [SerializeField]
    private GameObject _annihilateGameobject;
    [SerializeField]
    private float _duration = 5.0f;

    private CarController _carController;

    private void Awake()
    {
        _carController = GetComponent<CarController>();

        _annihilateGameobject.SetActive(_carController.annihilator);
    }

    public void Activate()
    {
        _annihilateGameobject.SetActive(true);
        _carController.annihilator = true;

        StartCoroutine(Deactivate());
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(_duration);

        _annihilateGameobject.SetActive(false);
        _carController.annihilator = false;
    }
}
