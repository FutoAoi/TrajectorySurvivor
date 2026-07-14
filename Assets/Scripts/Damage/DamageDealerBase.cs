using UnityEngine;

public class DamageDealerBase : MonoBehaviour
{
    [SerializeField] protected int _damage = 10;
    [SerializeField] protected Faction _ownerFaction;
    [SerializeField] protected GameObject _hitEffectPrefab;

    public void SetDamage(int damage) => _damage = damage;
    public void SetOwnerFaction(Faction faction) => _ownerFaction = faction;

    /// <summary>
    /// 対象にダメージを与えられるか判定する（陣営・死亡状態のチェック）
    /// </summary>
    protected bool TryGetDamageable(Collider other, out IDamageable damageable)
    {
        damageable = null;

        if (!other.TryGetComponent(out damageable)) return false;
        if (damageable.IsDead) return false;
        if (!other.TryGetComponent<IHealthStatus>(out var targetStatus)) return false;
        if (!FactionRelation.IsHostile(_ownerFaction, targetStatus.Faction)) return false;

        return true;
    }

    protected void ApplyDamage(IDamageable target, Vector3 hitPoint)
    {
        target.TakeDamage(new DamageInfo
        {
            amount = _damage,
            source = gameObject,
            hitPoint = hitPoint
        });

        SpawnHitEffect(hitPoint);
    }

    protected virtual void SpawnHitEffect(Vector3 position)
    {
        if (_hitEffectPrefab == null) return;
        var hiteffect = PoolManager.Instance.Get(_hitEffectPrefab, transform.position, Quaternion.identity);
    }
}
