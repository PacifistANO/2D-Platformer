using System.Collections;
using System.Collections.Generic;
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
            _points[i] = transform.GetChild(i).transform;
            Spawn(_points[i]);
        }
    }

    private void Spawn(Transform point)
    {
        _coinPrefab = Instantiate(_coinPrefab, point.position, Quaternion.identity);
    }
}
