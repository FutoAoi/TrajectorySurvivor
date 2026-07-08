using System;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private IHealthStatus _status;
    private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public bool IsDead { get; private set; }

    public event Action<DamageInfo, int> OnDamaged;
    public event Action OnDied;

    private void Awake()
    {
        _currentHealth = _status.MaxHealth;
    }

    public void TakeDamage(DamageInfo info)
    {
        if (IsDead) return;

        int finalDamage = Mathf.Max(1, info.amount - _status.Defense);
        _currentHealth -= finalDamage;
        OnDamaged?.Invoke(info, finalDamage);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;
        OnDied?.Invoke();
    }

    public void Heal(int amount)
    {
        if (IsDead) return;
        _currentHealth = Mathf.Min(_status.MaxHealth, _currentHealth + amount);
    }

}