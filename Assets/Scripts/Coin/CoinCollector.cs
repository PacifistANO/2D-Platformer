using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private List<Coin> _wallet = new List<Coin>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            coin.Collected();
            _wallet.Add(coin);
            Debug.Log(_wallet.Count);
        }
    }
}
