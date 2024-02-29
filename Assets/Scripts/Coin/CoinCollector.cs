using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int _coinsCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Coin>())
        {
            _coinsCount++;
            Destroy(collision.gameObject);
            Debug.Log(_coinsCount);
        }
    }
}
