using UnityEngine;

public class EnemyListener : MonoBehaviour
{
    [SerializeField] private HealthController _health;
    [SerializeField] private GameObject _popupPrefab;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(0, 1.5f, 0);

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
    }
}