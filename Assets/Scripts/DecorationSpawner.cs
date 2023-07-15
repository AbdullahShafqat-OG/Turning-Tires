using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DecorationSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _decorationPrefabs;

    [SerializeField]
    private Transform _decorationsParent;

    [SerializeField]
    private int _rows = 20;

    private ObjectPool<GameObject> _pool;

    private void OnEnable()
    {
        Spawner.OnSpawn += Generate;
    }

    private void OnDisable()
    {
        Spawner.OnSpawn -= Generate;
    }

    private void Start()
    {
        _pool = new ObjectPool<GameObject>(() => {
            return Instantiate(_decorationPrefabs[Random.Range(0, _decorationPrefabs.Length)]);
        }, decoration => {
            decoration.SetActive(true);
        }, decoration => {
            decoration.SetActive(false);
        }, decoration => {
            Destroy(decoration);
        }, false, 40, 100);
    }

    private void Generate(float boundsX)
    {
        for (int i = 0; i < _rows; i++)
        {
            float x = Random.Range(-boundsX, boundsX);
            float t = i / (float)(_rows - 1);
            float z = Spawner.Instance.CurrentZ + (t * Spawner.Instance.CameraLength);
            Vector3 position = new Vector3(x, 0, z);

            GameObject decoration = _pool.Get();
            decoration.transform.parent = _decorationsParent;
            decoration.transform.position = position;
            decoration.transform.rotation = Quaternion.Euler(0, Random.Range(0, 359), 0);
        }
    }
}
