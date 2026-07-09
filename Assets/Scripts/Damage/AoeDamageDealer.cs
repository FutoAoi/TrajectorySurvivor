using UnityEngine;

public class AoeDamageDealer : DamageDealerBase
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private LayerMask _targetLayer;

    private void Start()
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _targetLayer);
        foreach (var col in hits)
        {
            if (!TryGetDamageable(col, out var damageable)) continue;
            ApplyDamage(damageable, col.ClosestPoint(transform.position));
        }

        Destroy(gameObject);
    }
}