using UnityEngine;

public class EnemyListener : MonoBehaviour
{
    [SerializeField] private HealthController _health;
    [SerializeField] private GameObject _popupPrefab;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(0, 1.5f, 0);
    [SerializeField] private EnemyStatus _enemyStatus;

    private void OnEnable()
    {
        _health.OnDamaged += HandleDamaged;
        _health.OnDied += HandleDied;
    }
    private void OnDisable()
    {
        _health.OnDamaged -= HandleDamaged;
        _health.OnDied -= HandleDied;
    }

    private void HandleDamaged(DamageInfo info, int finalDamage)
    {
        var obj = PoolManager.Instance.Get(_popupPrefab, transform.position + _spawnOffset, Quaternion.identity);
        obj.GetComponent<DamagePopup>().SetData(finalDamage, info.isCrit);
    }

    private void HandleDied()
    {
        PoolManager.Instance.Return(gameObject);
        if (_enemyStatus.ExpGemPrefab == null) return;

        var obj = PoolManager.Instance.Get(_enemyStatus.ExpGemPrefab, transform.position, Quaternion.identity);

        if (obj.TryGetComponent<ExpGem>(out var gem))
        {
            gem.SetExpAmount(_enemyStatus.ExpAmount);
        }
    }
}