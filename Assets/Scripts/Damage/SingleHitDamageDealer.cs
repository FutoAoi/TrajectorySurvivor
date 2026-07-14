using UnityEngine;

public class SingleHitDamageDealer : DamageDealerBase, IPooledObject
{
    [SerializeField] private float _lifeTime = 5f;

    private bool _hasHit;
    private float _timer;

    public void OnSpawned()
    {
        _hasHit = false;
        _timer = 0f;
    }

    public void OnDespawned() { }

    private void Update()
    {
        if (GamePauseManager.IsPaused) return;
        _timer += Time.deltaTime;
        if (_timer >= _lifeTime)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasHit) return;
        if (!TryGetDamageable(other, out var damageable)) return;

        _hasHit = true;
        Vector3 hitPoint = other.ClosestPoint(transform.position);
        ApplyDamage(damageable, hitPoint);

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        PoolManager.Instance.Return(gameObject);
    }
}