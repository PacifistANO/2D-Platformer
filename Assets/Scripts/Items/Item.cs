using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Action<Item> IsCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            IsCollected?.Invoke(this);
    }
}
