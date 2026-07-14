using UnityEngine;

public class ExpGem : MonoBehaviour, IPooledObject
{
    [SerializeField] private int _defaultExpAmount = 10;
    private int _expAmount;

    public void SetExpAmount(int amount)
    {
        _expAmount = amount;
    }

    public void OnSpawned()
    {
        _expAmount = _defaultExpAmount; // Setされなかった場合のフォールバック
    }

    public void OnDespawned() { }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ExpManager.Instance.AddExp(_expAmount);
        PoolManager.Instance.Return(gameObject);
    }
}