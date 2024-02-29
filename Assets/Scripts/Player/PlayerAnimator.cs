using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static class Parameters
    {
        public const string AirSpeedY = nameof(AirSpeedY);
        public const string Grounded = nameof(Grounded);
        public const string AnimState = nameof(AnimState);
        public const string Jump = nameof(Jump);
    }
}
