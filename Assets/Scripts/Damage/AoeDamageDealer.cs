using UnityEngine;

public class AoeDamageDealer : DamageDealerBase, IPooledObject
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private LayerMask _targetLayer;

    public void OnSpawned()
    {
        Explode();
    }

    public void OnDespawned() { }

    private void Explode()
    {
        SpawnHitEffect(transform.position);

        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _targetLayer);
        foreach (var col in hits)
        {
            if (!TryGetDamageable(col, out var damageable)) continue;
            ApplyDamage(damageable, col.ClosestPoint(transform.position));
        }

        PoolManager.Instance.Return(gameObject);
    }
}