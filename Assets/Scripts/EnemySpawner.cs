using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    private Transform[] _targetPoints;

    private void Start()
    {
        _targetPoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _targetPoints[i] = transform.GetChild(i).transform;
        }

        Spawn();
    }

    private void Spawn()
    {
        _enemyPrefab = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        _enemyPrefab.GetComponent<EnemyMover>().InitTargets(_targetPoints);
    }
}
