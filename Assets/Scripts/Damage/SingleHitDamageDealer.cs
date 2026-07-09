using UnityEngine;

public class SingleHitDamageDealer : DamageDealerBase
{
    [SerializeField] private float _lifeTime = 5f; // âΩÇ…Ç‡ìñÇΩÇÁÇ»Ç©Ç¡ÇΩèÍçáÇÃé©ìÆè¡ñ≈

    private bool _hasHit;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasHit) return;
        if (!TryGetDamageable(other, out var damageable)) return;

        _hasHit = true;
        Vector3 hitPoint = other.ClosestPoint(transform.position);
        ApplyDamage(damageable, hitPoint);

        Destroy(gameObject);
    }
}