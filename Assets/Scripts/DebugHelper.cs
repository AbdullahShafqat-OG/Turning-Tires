using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DebugHelper : MonoBehaviour
{
    public GameObject car;
    public Vector3 punch;
    public float duration;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DestroyAllChildren(transform);
            GameObject go = Instantiate(car, transform);
            go.transform.DOPunchScale(punch, duration);
            //go.AddComponent<ObjectSpinner>();
        }
    }

    void DestroyAllChildren(Transform parent)
    {
        // Iterate through all the children of the parent transform
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            // Destroy each child GameObject
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}
