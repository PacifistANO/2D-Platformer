using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector2 _trackerOffset;

    private void Update()
    {
        var position = transform.position;
        position.x = _player.transform.position.x + _trackerOffset.x;
        position.y = _player.transform.position.y + _trackerOffset.y;
        transform.position = position; 
    }
}
