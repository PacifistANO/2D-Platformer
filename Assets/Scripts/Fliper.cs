using System;
using UnityEngine;

public class Fliper
{
    public Quaternion FlipX(float targetPosition, float pointOfReference)
    {
        bool direction = targetPosition > pointOfReference;
        Quaternion quaternion = Quaternion.Euler(0, 180 * Convert.ToInt32(direction), 0);
        return quaternion;
    }
}
