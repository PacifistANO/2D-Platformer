using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public static class Parameters
    {
        public static readonly int AnimState = Animator.StringToHash(nameof(AnimState));
    }
}
