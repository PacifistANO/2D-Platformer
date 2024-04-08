using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int _value;

    public int Value => _value;

    public void IncreaseHealth(int addition)
    {
        if (addition >= 0)
            _value += addition;
    }

    public void DecreaseHealth(int reduction)
    {
        if (reduction >= 0)
            _value -= reduction;
    }
}
