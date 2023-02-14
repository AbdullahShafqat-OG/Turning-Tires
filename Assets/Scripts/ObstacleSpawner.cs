using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private int pooledAmount = 30;
    [SerializeField]
    private Obstacle[] obstaclePrefabs;
    [SerializeField]
    private float yMax = 1, yMin = 2;

    [Space]
    [SerializeField]
    private ScreenBounds screenBounds;
    public ScreenBounds ScreenBounds
    {
        get { return screenBounds; }
    }
    [SerializeField]
    private int numIterations = 50;
    [SerializeField]
    private Transform camZEnd;
    public Transform CamZEnd
    {
        get { return camZEnd; }
    }
    [SerializeField]
    private Transform camZStart;
    [SerializeField]
    private float safetyLevel = 1f;

    private Dictionary<int, Queue<Obstacle>> poolDictionary;
    private float[] obstacleStrengths;

    private float screenBoundsX = -1;
    private float cameraLength;
    private float currentZ;
    public float CurrentZ
    {
        get { return currentZ; }
    }
    public float zOffset;

    private void Start()
    {
        cameraLength = camZStart.position.z - camZEnd.position.z;
        currentZ = camZEnd.position.z + zOffset;

        poolDictionary = new Dictionary<int, Queue<Obstacle>>();
        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            Queue<Obstacle> objectPool = new Queue<Obstacle>();
            for (int j = 0; j < 1; j++)
            {
                Obstacle obstacle = Instantiate(obstaclePrefabs[i]);
                obstacle.transform.parent = this.transform;
                obstacle.gameObject.SetActive(false);
                objectPool.Enqueue(obstacle);
            }

            poolDictionary.Add(i, objectPool);
        }
        obstacleStrengths = CalculateObstacleStrengths();

        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            for (int j = 0; j < pooledAmount - 1; j++)
            {
                Obstacle obstacle = Instantiate(obstaclePrefabs[i]);
                obstacle.transform.parent = this.transform;
                obstacle.gameObject.SetActive(false);
                obstacle.strength = obstacleStrengths[i];
                poolDictionary[i].Enqueue(obstacle);
            }
        }
    }

    private void Update()
    {
        if (camZStart.position.z > currentZ)
        {
            if (screenBoundsX == -1) screenBoundsX = screenBounds.GetBoundsSize().x / 2f;
            Generate(screenBoundsX);

            currentZ += cameraLength;
        }
    }

    public Obstacle SpawnFromPool(int index, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(index))
        {
            Debug.LogWarning($"Pool with tag {index} does not exist");
            return null;
        }

        Obstacle obstacleToSpawn = poolDictionary[index].Dequeue();
        GameObject obstacle = obstacleToSpawn.gameObject;

        obstacle.SetActive(true);
        obstacle.transform.position = position;
        obstacle.transform.rotation = rotation;

        poolDictionary[index].Enqueue(obstacleToSpawn);

        return obstacleToSpawn;
    }

    private float[] CalculateObstacleStrengths()
    {
        float[] strengths = new float[obstaclePrefabs.Length];
        for (int i = 0; i < strengths.Length; i++)
            strengths[i] = poolDictionary[i].Peek().strength;

        float xMax = strengths.Max();
        float xMin = strengths.Min();
        float slope = (yMax - yMin) / (xMax - xMin);
        float intercept = 1 - (slope * xMax);

        for (int i = 0; i < strengths.Length; i++)
        {
            Obstacle obstacle = poolDictionary[i].Peek();
            float multiplier = (obstacle.strength * slope) + intercept;
            obstacle.SetMultiplier(multiplier);
            strengths[i] = obstacle.strength;
        }

        return strengths;
    }

    private void Generate(float boundsX)
    {
        // can object pool these as well
        float[] strengths = new float[numIterations];
        Vector3[] positions = new Vector3[numIterations];
        bool[] valids = new bool[numIterations];

        for (int i = 0; i < numIterations; i++)
        {
            float strength = obstacleStrengths[Random.Range(0, obstacleStrengths.Length)];
            strengths[i] = strength;

            Vector3 position =
                new Vector3(
                    Random.Range(-boundsX, boundsX), 
                    0, 
                    Random.Range(currentZ + strength, currentZ - strength + cameraLength)
                    );
            positions[i] = position;

            valids[i] = true;

            for (int j = i - 1; j >= 0; j--)
            {
                if (valids[j] == false)
                    continue;

                if (Vector3.Distance(positions[i], positions[j]) < (strengths[i] + strengths[j] + safetyLevel))
                {
                    valids[i] = false;
                    break;
                }
            }
        }

        for (int i = 0; i < numIterations; i++)
        {
            if (valids[i] != false)
                InstantiateObstacles(strengths[i], positions[i]);
        }
    }

    private GameObject InstantiateObstacles(float strength, Vector3 position)
    {
        // TODO
        // there might be the case of having multiple objects with same strength
        // improbable but possible
        int index = System.Array.IndexOf(obstacleStrengths, strength);
        if (index == -1)
        {
            Debug.Log("OBSTACLE INSTANTIATING ERROR");
            return null;
        }
        
        Obstacle obstacle = SpawnFromPool(index, position, Quaternion.Euler(0, Random.Range(0, 359), 0));
        obstacle.strength = strength;

        return obstacle.gameObject;
    }
}
