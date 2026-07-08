using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageInfo info);
}

public struct DamageInfo
{
    public int amount;
    public GameObject source;
    public Vector3 hitPoint;
    public Vector3 knockback;
}
