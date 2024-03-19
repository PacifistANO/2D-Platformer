using System;
using UnityEngine;

public class Coin : MonoBehaviour 
{
    public event Action<Coin> CoinCollected;

    public void Collect()
    {
        CoinCollected?.Invoke(this);
    }
}

