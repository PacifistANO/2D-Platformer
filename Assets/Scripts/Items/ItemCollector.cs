using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private List<Coin> _wallet = new List<Coin>();
    private CharacterHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GetComponent<CharacterHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            _wallet.Add(coin);
            Debug.Log($"Монет собрано: {_wallet.Count}");
        }

        else if(collision.TryGetComponent(out Healer healer))
        {
            _playerHealth.Increase(healer.HealthIncrease);
            Debug.Log($"Прибавка к здоровью + {healer.HealthIncrease}");
        }
    }
}
