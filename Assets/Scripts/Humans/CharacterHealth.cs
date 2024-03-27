using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int _health;

    public int Health => _health;

    public void IncreaseHealth(int addition)
    {
        _health += addition;
    }

    public void DecreaseHealth(int reduction)
    {
        _health -= reduction;
    }    
}
