using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject[] _powerupPrefabs;

    [SerializeField]
    private Transform coinsParent;
    [SerializeField]
    private float radius = 2f;
    [SerializeField]
    private float zIncrement = 2f;
    [SerializeField]
    private float interval = 3f;

    [Range(0, 100)]
    [SerializeField]
    private int _powerupPercentage = 10;

    private Color color = Color.blue;
    private List<Transform> coins;

    //private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        Messenger<Transform, Transform>.AddListener(GameEvent.COIN_PULLED, PullCoin);
        //Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void OnDestroy()
    {
        Messenger<Transform, Transform>.RemoveListener(GameEvent.COIN_PULLED, PullCoin);
        //Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void Start()
    {
        //_pool = new ObjectPool<GameObject>(() => {
        //    return Instantiate(coinPrefab, transform.position, Quaternion.identity);
        //}, coin => {
        //    coin.SetActive(true);
        //}, coin => {
        //    coin.SetActive(false);
        //}, coin => {
        //    Destroy(coin);
        //}, false, 40, 100);

        coins = new List<Transform>();
        Invoke("SpawnCoin", interval);
    }

    private void Update()
    {
        if (color == Color.red)
        {
            var p = transform.position;
            p.z += zIncrement;
            transform.position = p;
            Check();
        }

        if (coins.Count != 0 && coins[0].position.z < Spawner.Instance.CamZEnd.position.z)
        {
            DestroyCoin();
        }
    }

    private void SpawnCoin()
    {
        float screenBoundsX = Spawner.Instance.ScreenBounds.GetBoundsSize().x / 2f;

        Vector3 position =
                new Vector3(
                    Random.Range(-screenBoundsX, screenBoundsX),
                    0,
                    Spawner.Instance.CurrentZ
                    );
        transform.position = position;

        Check();
    }

    private void DownScaleCoin(int value = default(int))
    {
        coins[0].transform.DOScale(0, 0.1f).OnComplete(DestroyCoin);
    }

    private void DestroyCoin()
    {
        //_pool.Release(coins[0].gameObject);
        Destroy(coins[0].gameObject);
        coins.RemoveAt(0);
    }

    private void DestroyCoin(Transform coin)
    {
        Messenger<int>.Broadcast(GameEvent.COIN_COLLECTED, coin.GetComponent<Coin>().value);
        Destroy(coin.gameObject);
        //_pool.Release(coin.gameObject);
        coins.Remove(coin);
    }

    private void PullCoin(Transform player, Transform coin)
    {
        Coin test = coin.GetComponent<Coin>();
        if (test != null)
        {
            coin.transform.DOMove(player.transform.position, 0.2f);
            coin.transform.DOScale(0, 0.2f).SetEase(Ease.InSine).OnComplete(() => DestroyCoin(coin));
        }
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
            if (Random.Range(0, 100) < _powerupPercentage)
            {
                Vector3 pos = transform.position;
                GameObject powerup = 
                    Instantiate(_powerupPrefabs[Random.Range(0, _powerupPrefabs.Length)]);
                pos.y = powerup.transform.position.y;
                powerup.transform.position = pos;
                powerup.transform.rotation = Quaternion.identity;
            }
            else
            {
                GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                //GameObject coin = _pool.Get();
                coin.transform.parent = coinsParent;
                coins.Add(coin.transform);
            }

            Invoke("SpawnCoin", interval);
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
