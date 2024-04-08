using System;
using UnityEngine;

public class EnemyPursuit : MonoBehaviour
{
    [SerializeField] private float _speed;

    public Action<Transform, float> TargetDetected;

    public void DetectTarget(Transform target)
    {
        TargetDetected?.Invoke(target, _speed);
    }

}
