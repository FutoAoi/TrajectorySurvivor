using System.Collections.Generic;
using UnityEngine;

public class BlinkDamageDealer : DamageDealerBase
{
    private readonly HashSet<IDamageable> _hitTargets = new();
    private bool _isActive;

    public void Activate()
    {
        _hitTargets.Clear();
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;
        if (!TryGetDamageable(other, out var damageable)) return;
        if (_hitTargets.Contains(damageable)) return;

        _hitTargets.Add(damageable);
        ApplyDamage(damageable, other.ClosestPoint(transform.position));
    }
}