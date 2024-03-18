using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour 
{
    public UnityAction<Coin> CoinCollected;

    public void Collected()
    {
        CoinCollected?.Invoke(this);
    }
}

