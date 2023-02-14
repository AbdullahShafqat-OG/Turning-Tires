using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ScreenBounds : MonoBehaviour
{
    [SerializeField]
    private float _offsetTeleport = 0.2f;

    private Camera _cam;
    private BoxCollider _boxCollider;

    private float _width, _height;

    private void Awake()
    {
        _cam = Camera.main;
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundsSize();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q)) UpdateBoundsSize();
        //if (Input.GetKeyDown(KeyCode.E)) FindBoundaries();
    }

    private void UpdateBoundsSize()
    {
        float zSize = _cam.orthographicSize * 2;
        Vector3 boxColliderSize = new Vector3(zSize * _cam.aspect, 10, zSize);
        _boxCollider.size = boxColliderSize;
    }

    private void FindBoundaries()
    {
        _width = 1 / (_cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
        _height = 1 / (_cam.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
        print("height; " + _height + " width " + _width);
    }

    public bool AmIOutOfBounds(Vector3 worldPosition)
    {
        return !_boxCollider.bounds.Contains(worldPosition);
    }

    public Vector3 CalculateWrappedPosition(Vector3 worldPosition)
    {
        bool xBoundsResult = Mathf.Abs(worldPosition.x) > Mathf.Abs(_boxCollider.bounds.min.x);

        Vector3 signWorldPosition = 
            new Vector3(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y), Mathf.Sign(worldPosition.z));

        if (xBoundsResult)
        {
            return new Vector3(worldPosition.x * -1, worldPosition.y, worldPosition.z) + 
                new Vector3(_offsetTeleport * signWorldPosition.x, 0, 0);
        }

        return worldPosition;
    }

    public Vector3 GetBoundsSize()
    {
        return _boxCollider.bounds.size;
    }
}
