using System;
using UnityEngine;

public class Coin : MonoBehaviour 
{
    public event Action<Coin> CoinCollected;

    public void Collected()
    {
        CoinCollected?.Invoke(this);
    }
}

