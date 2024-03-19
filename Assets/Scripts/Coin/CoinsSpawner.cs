using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private Transform[] _points;

    private void Start()
    {
        _points = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _points[i] = transform.GetChild(i);
            Spawn(_points[i]);
        }
    }

    private void Spawn(Transform point)
    {
        var newCoin = Instantiate(_coinPrefab, point.position, Quaternion.identity);
        newCoin.CoinCollected += DeleteCoin;
    }

    private void DeleteCoin(Coin coin)
    {
        coin.CoinCollected -= DeleteCoin;
        Destroy(coin.gameObject);
    }
}
