using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public static class Parameters
    {
        public static readonly int AirSpeedY = Animator.StringToHash(nameof(AirSpeedY));
        public static readonly int Grounded = Animator.StringToHash(nameof(Grounded));
        public static readonly int AnimState = Animator.StringToHash(nameof(AnimState));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
    }
}
