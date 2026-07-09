using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageInfo info);
    bool IsDead { get; }
}

public struct DamageInfo
{
    public int amount;
    public GameObject source;
    public Vector3 hitPoint;
    public Vector3 knockback;
    public bool isCrit;
}
