using System.Collections.Generic;
using UnityEngine;

public class ContactDamageDealer : DamageDealerBase
{
    [SerializeField] private float _tickInterval = 1f; // âΩïbÇ≤Ç∆Ç…É_ÉÅÅ[ÉWÇó^Ç¶ÇÈÇ©

    private readonly Dictionary<IDamageable, float> _cooldowns = new();
    private readonly Dictionary<IDamageable, Collider> _colliderCache = new();

    private void OnTriggerStay(Collider other)
    {
        if (GamePauseManager.IsPaused) return;
        if (!TryGetDamageable(other, out var damageable)) return;

        if (_cooldowns.TryGetValue(damageable, out float timer))
        {
            timer -= Time.deltaTime;
            if (timer > 0f)
            {
                _cooldowns[damageable] = timer;
                return;
            }
        }

        Vector3 hitPoint = other.ClosestPoint(transform.position);
        ApplyDamage(damageable, hitPoint);

        _cooldowns[damageable] = _tickInterval;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            _cooldowns.Remove(damageable);
        }
    }
}