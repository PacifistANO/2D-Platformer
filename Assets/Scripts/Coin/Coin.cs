using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour 
{
    public event UnityAction<Coin> CoinCollected;

    public void Collected()
    {
        CoinCollected?.Invoke(this);
    }
}

