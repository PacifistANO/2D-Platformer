using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private List<Coin> _wallet = new List<Coin>();
    private CharacterHealth _playerHealth;

    public Action<Item> ItemCollected;

    private void Start()
    {
        _playerHealth = GetComponent<CharacterHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            _wallet.Add(coin);
            ItemCollected?.Invoke(coin);
            Debug.Log($"Монет собрано: {_wallet.Count}");
        }

        else if(collision.TryGetComponent(out Healer healer))
        {
            _playerHealth.IncreaseHealth(healer.HealthIncrease);
            ItemCollected?.Invoke(healer);
            Debug.Log($"Прибавка к здоровью + {healer.HealthIncrease}");
        }
    }
}
