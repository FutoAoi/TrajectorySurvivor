using System.Collections.Generic;
using UnityEngine;

public class AuraDamageDealer : DamageDealerBase
{
    [SerializeField] private float _tickInterval = 0.5f;
    [SerializeField] private float _duration = 3f;

    private readonly Dictionary<IDamageable, Collider> _targetsInRange = new();
    private float _tickTimer;
    private float _durationTimer;

    private void Update()
    {
        _durationTimer += Time.deltaTime;
        if (_durationTimer >= _duration)
        {
            Destroy(gameObject);
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
}