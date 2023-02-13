using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private ObstacleSpawner spawner;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private Transform coinsParent;
    [SerializeField]
    private float radius = 2f;
    [SerializeField]
    private float zIncrement = 2f;
    [SerializeField]
    private float interval = 3f;

    private Color color = Color.blue;
    private List<Transform> coins;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.COIN_COLLECTED, DestroyCoin);
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.COIN_COLLECTED, DestroyCoin);
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void Start()
    {
        coins = new List<Transform>();
        InvokeRepeating("SpawnCoin", interval, interval);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("HELELEo");
            Check();
        }

        if (color == Color.red)
        {
            var p = transform.position;
            p.z += zIncrement;
            transform.position = p;
            Check();
        }

        if (coins.Count != 0 && coins[0].position.z < spawner.CamZEnd.position.z)
        {
            DestroyCoin();
        }
    }

    private void SpawnCoin()
    {
        float screenBoundsX = spawner.ScreenBounds.GetBoundsSize().x / 2f;
        Vector3 position =
                new Vector3(
                    Random.Range(-screenBoundsX, screenBoundsX),
                    0,
                    spawner.CurrentZ
                    );
        transform.position = position;

        Check();
    }

    private void DestroyCoin(int value = default(int))
    {
        Destroy(coins[0].gameObject);
        coins.RemoveAt(0);
    }

    private void Check()
    {
        if (Physics.CheckSphere(transform.position, radius))
        {
            color = Color.red;
        }
        else
        {
            color = Color.blue;
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.transform.parent = coinsParent;
            coins.Add(coin.transform);
        }
    }

    private void OnGameOver()
    {
        CancelInvoke("SpawnCoin");
    }

    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = oldColor;
    }
}
