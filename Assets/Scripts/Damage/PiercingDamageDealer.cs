using System.Collections.Generic;
using UnityEngine;

public class PiercingDamageDealer : DamageDealerBase
{
    [SerializeField] private int _maxPierceCount = 3; // -1‚Å–³ŒÀŠÑ’Ê
    [SerializeField] private float _lifeTime = 5f;

    private readonly HashSet<IDamageable> _hitTargets = new();
    private int _pierceCount;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!TryGetDamageable(other, out var damageable)) return;
        if (_hitTargets.Contains(damageable)) return; // “¯‚¶‘ŠŽè‚É‚Í“ñ“x“–‚½‚ç‚È‚¢

        _hitTargets.Add(damageable);

        Vector3 hitPoint = other.ClosestPoint(transform.position);
        ApplyDamage(damageable, hitPoint);

        _pierceCount++;
        if (_maxPierceCount >= 0 && _pierceCount >= _maxPierceCount)
        {
            Destroy(gameObject);
        }
    }
}