using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    public float strength;

    public Color color = Color.blue;

    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private float _multiplier = 1;

    private void Awake()
    {
        _collider = GetComponent<Collider>();

        Vector3 size = _collider.bounds.size;
        float val = Mathf.Max(size.x, size.z);
        strength = val;
    }

    public void UpdateStrength(float s)
    {
        strength = s;
    }

    public void SetMultiplier(float multiplier)
    {
        _multiplier = multiplier;
        strength *= multiplier;
    }

    private void OnDrawGizmos()
    {
        _collider = GetComponent<Collider>();

        Vector3 size = _collider.bounds.size;
        float val = Mathf.Max(size.x, size.z);
        Vector3 pos = transform.position;

        Helpers.DrawWireDisk(pos, strength, color);
    }
}
