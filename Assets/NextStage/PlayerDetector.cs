using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private float _detectorRadius = 5;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _detectorRadius);

        if (collider.TryGetComponent(out Player player))
        {
            Debug.Log(player);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _detectorRadius);
    }
}
