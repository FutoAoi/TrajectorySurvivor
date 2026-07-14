using System.Collections.Generic;
using UnityEngine;

public class BlinkDamageDealer : DamageDealerBase, IPooledObject
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

    // プールから取得された瞬間は自動発動しない
    // （PlayerBlinkControllerがDashRoutine内で明示的にActivate/Deactivateを呼ぶ運用のため）
    public void OnSpawned() { }

    public void OnDespawned()
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