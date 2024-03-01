using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int _coinsCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            _coinsCount++;
            Destroy(coin.gameObject);
            Debug.Log(_coinsCount);
        }
    }
}
