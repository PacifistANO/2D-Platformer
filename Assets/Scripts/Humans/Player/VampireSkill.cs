using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterHealth))]
public class VampireSkill : MonoBehaviour
{
    [SerializeField] private float _radiusSkill;
    [SerializeField] private float _damagePerCycle;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private Color _gizmoColor;

    public Action SkillStopped;
    
    private float _deltaTime;
    private float _skillTime;
    private bool _isWorked;
    private CharacterHealth _playerHealth;
    

    private void Start()
    {
        _deltaTime = 0.5f;
        _skillTime = 6;
        _isWorked = false;
        _playerHealth = GetComponent<CharacterHealth>();
    }

    public void OnSkillButtonClick()
    {
        if (_isWorked == false)
            StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage()
    {
        _isWorked = true;
        float timeCounter = 0;
        var timeBerweenDamage = new WaitForSeconds(_deltaTime);

        while (timeCounter < _skillTime)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _radiusSkill, _enemyLayerMask);

            if (collider != null)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_damagePerCycle);
                    _playerHealth.Increase(_damagePerCycle);
                }
            }

            timeCounter += _deltaTime;
            yield return timeBerweenDamage;
        }

        _isWorked = false;
        SkillStopped?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawSphere(transform.position, _radiusSkill);
    }
}
