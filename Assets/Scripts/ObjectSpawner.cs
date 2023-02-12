using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private ScreenBounds screenBounds;
    [SerializeField]
    private Transform pointPrefab;
    [SerializeField, Range(10, 100)]
    private int resolution = 10;

    [SerializeField]
    private Transform _camYEnd;
    [SerializeField]
    private Transform _camYStart;

    private List<Transform[,]> _all2dPoints;
    private Transform[,] _2dPoints;

    [SerializeField]
    private float _zOffsetPercent = 0.5f;
    private float _zOffset;
    private float _zPosition;

    private Color _c;

    private List<int> _selectedHistory;

    private void Start()
    {
        _zPosition = _camYStart.position.z;

        _all2dPoints = new List<Transform[,]>();

        _selectedHistory = new List<int>();
    }

    private void Update()
    {
        if (_all2dPoints.Count < 3)
        {
            Generate2DCubeMap();
        }
        if (_all2dPoints[0][_all2dPoints[0].GetLength(0)-1, _all2dPoints[0].GetLength(1)-1].position.z < _camYEnd.position.z)
        {
            Delete2DCubeMap();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (int x in _selectedHistory)
                Debug.Log(x);
        }
    }

    private void Delete2DCubeMap()
    {
        Transform[,] temp = _all2dPoints[0];
        for (int k = 0; k < temp.GetLength(0); k++)
        {
            for (int l = 0; l < temp.GetLength(1); l++)
            {
                Transform t = temp[k, l];
                Destroy(t.gameObject);
            }
        }
        _all2dPoints.RemoveAt(0);
    }

    private void Generate2DCubeMap()
    {
        float screenBoundsX = screenBounds.GetBoundsSize().x / 2f;
        float step = 2f / resolution * screenBoundsX;
        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one * step;

        _zOffset = scale.z * _zOffsetPercent;

        int wantedAmount = (int)((_camYStart.position.z - _camYEnd.position.z) / (scale.z + _zOffset));
        _c = Random.ColorHSV();

        _2dPoints = new Transform[wantedAmount, resolution];
        for (int j = 0; j < wantedAmount; j++)
        {
            for (int i = 0; i < resolution; i++)
            {
                Transform point = _2dPoints[j, i] = Instantiate(pointPrefab);
                position.x = (i + 0.5f) * step - screenBoundsX;
                position.z = _zPosition;
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
            }

            RandomSelectMultiple(j);

            _zPosition += scale.z + _zOffset;
        }

        _all2dPoints.Add(_2dPoints);
    }

    private void RandomSelectMultiple(int j)
    {
        int n;
        if (_selectedHistory.LastOrDefault() >= resolution / 5) n = Random.Range(0, resolution / 6);
        else n = Random.Range(0, resolution / 3);
        
        _selectedHistory.Add(n);

        List<int> selected = new List<int>();
        while (selected.Count < n)
        {
            int temp = Random.Range(0, resolution);
            if (selected.Contains(temp))
                continue;
            selected.Add(temp);
        }

        for (int i = 0; i < selected.Count; i++)
        {
            RandomizeColor(_2dPoints[j, selected[i]], _c);
        }
    }

    private void RandomizeColor(Transform point, Color c)
    {
        MeshRenderer[] mrs = point.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
            mr.enabled = true;
        // point.GetComponent<MeshRenderer>().material.color = c;
        point.GetComponent<BoxCollider>().enabled = true;
    }
}
