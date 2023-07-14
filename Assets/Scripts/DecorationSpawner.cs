using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DecorationSpawner : MonoBehaviour
{
    [SerializeField]
    private Decoration _decorationPrefab;

    [SerializeField]
    private ScreenBounds _screenBounds;
    [SerializeField]
    private Transform _decorationsParent;

    private ObjectPool<Decoration> _pool;

    private void Start()
    {
        _pool = new ObjectPool<Decoration>(() => {
            return Instantiate(_decorationPrefab);
        }, decoration => {
            decoration.gameObject.SetActive(true);
        }, decoration => {
            decoration.gameObject.SetActive(false);
        }, decoration => {
            Destroy(decoration);
        }, false, 40, 100);
    }

    private void Spawn()
    {

    }
}
