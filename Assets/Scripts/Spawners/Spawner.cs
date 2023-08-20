using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [field: SerializeField]
    public ScreenBounds ScreenBounds { get; private set; }

    [field: SerializeField]
    public Transform CamZEnd { get; private set; }
    
    [SerializeField]
    private Transform _camZStart;

    public float CurrentZ { get; private set; }

    private float _screenBoundsX = -1;

    public float CameraLength { get; private set; }

    [SerializeField]
    private float _zOffset;

    public delegate void SpawnAction(float screenBoundsX);
    public static event SpawnAction OnSpawn;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CameraLength = _camZStart.position.z - CamZEnd.position.z;
        CurrentZ = _camZStart.position.z + _zOffset;
    }

    private void Update()
    {
        if (_camZStart.position.z > CurrentZ)
        {
            if (_screenBoundsX == -1) _screenBoundsX = ScreenBounds.GetBoundsSize().x / 2f;

            OnSpawn?.Invoke(_screenBoundsX);

            CurrentZ += CameraLength;
        }
    }
}
