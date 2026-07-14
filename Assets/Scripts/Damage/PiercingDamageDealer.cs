using System.Collections.Generic;
using UnityEngine;

public class PiercingDamageDealer : DamageDealerBase, IPooledObject
{
    [SerializeField] private int _maxPierceCount = 3; // -1‚Å–³ŒÀŠÑ’Ê
    [SerializeField] private float _lifeTime = 5f;

    private readonly HashSet<IDamageable> _hitTargets = new();
    private int _pierceCount;
    private float _timer;
    private bool _isActive;

    public void Activate()
    {
        _hitTargets.Clear();
        _pierceCount = 0;
        _timer = 0f;
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    // IPooledObject: ƒv[ƒ‹‚©‚çŽæ“¾‚³‚ê‚½uŠÔ‚ÉŒÄ‚Î‚ê‚é
    public void OnSpawned()
    {
        Activate();
    }

    public void OnDespawned()
    {
        _isActive = false;
    }

    private void Update()
    {
        if (GamePauseManager.IsPaused) return;
        if (!_isActive) return;

        _timer += Time.deltaTime;
        if (_timer >= _lifeTime)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;
        if (!TryGetDamageable(other, out var damageable)) return;
        if (_hitTargets.Contains(damageable)) return;

        _hitTargets.Add(damageable);
        ApplyDamage(damageable, other.ClosestPoint(transform.position));

        _pierceCount++;
        if (_maxPierceCount >= 0 && _pierceCount >= _maxPierceCount)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        _isActive = false;
        PoolManager.Instance.Return(gameObject);
    }
}