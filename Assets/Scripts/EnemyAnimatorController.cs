using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    public static class Parameters
    {
        public static readonly int AnimState = Animator.StringToHash(nameof(AnimState));
    }
}
