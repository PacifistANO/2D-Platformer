using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private Coin _newCoin;
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

    private void OnDisable()
    {
        _newCoin.CoinCollected -= DeleteCoin;
    }

    private void Spawn(Transform point)
    {
        _newCoin = Instantiate(_coinPrefab, point.position, Quaternion.identity);
        _newCoin.CoinCollected += DeleteCoin;
    }

    private void DeleteCoin(Coin coin)
    {
        Destroy(coin.gameObject);
    }
}
