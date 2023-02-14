using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
            Messenger<int>.Broadcast(GameEvent.COIN_COLLECTED, other.GetComponent<Coin>().Value);
    }
}
