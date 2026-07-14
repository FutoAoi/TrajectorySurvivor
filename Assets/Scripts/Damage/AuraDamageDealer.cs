using System.Collections.Generic;
using UnityEngine;

public class AuraDamageDealer : DamageDealerBase, IPooledObject
{
    [SerializeField] private float _tickInterval = 0.5f;
    [SerializeField] private float _duration = 3f;

    private readonly Dictionary<IDamageable, Collider> _targetsInRange = new();
    private float _tickTimer;
    private float _durationTimer;
    private bool _isReturned;

    public void OnSpawned()
    {
        _tickTimer = 0f;
        _durationTimer = 0f;
        _isReturned = false;
        _targetsInRange.Clear(); // ← 重要：前回使用時の対象リストが残らないように
    }

    public void OnDespawned()
    {
        _targetsInRange.Clear(); // 返却時にも念のためクリア
    }

    private void Update()
    {
        if (GamePauseManager.IsPaused) return;

        _durationTimer += Time.deltaTime;
        if (_durationTimer >= _duration)
        {
            ReturnToPool();
            return;
        }

        _tickTimer += Time.deltaTime;
        if (_tickTimer >= _tickInterval)
        {
            _tickTimer = 0f;
            DealTickDamage();
        }
    }

    private void DealTickDamage()
    {
        foreach (var pair in _targetsInRange)
        {
            if (pair.Key.IsDead) continue;
            Vector3 hitPoint = pair.Value.ClosestPoint(transform.position);
            ApplyDamage(pair.Key, hitPoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!TryGetDamageable(other, out var damageable)) return;
        _targetsInRange[damageable] = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
            _targetsInRange.Remove(damageable);
    }

    private void ReturnToPool()
    {
        if (_isReturned) return;
        _isReturned = true;
        PoolManager.Instance.Return(gameObject);
    }
}