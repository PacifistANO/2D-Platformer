using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;

    private Transform[] _targetPoints;
    private int _enemyIndex;

    private void Start()
    {
        _targetPoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _targetPoints[i] = transform.GetChild(i);
        }

        Spawn();
    }

    private void Spawn()
    {
        _enemyIndex = Random.Range(0, _enemyPrefabs.Count);
        var newEnemy = Instantiate(_enemyPrefabs[_enemyIndex], transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyMover>().InitTargets(_targetPoints);
    }
}
