using UnityEngine;

public class EnemyStatus : MonoBehaviour, IHealthStatus
{
    [SerializeField] private int _enemyMaxHealth;
    [SerializeField] private int _enemyDefense;
    [SerializeField] private Faction _faction;

    [Header("ƒhƒƒbƒvÝ’è")]
    [SerializeField] private GameObject _expGemPrefab;
    [SerializeField] private int _expAmount = 10;

    public int MaxHealth => _enemyMaxHealth;
    public int Defense => _enemyDefense;

    public Faction Faction => _faction;

    public GameObject ExpGemPrefab => _expGemPrefab;
    public int ExpAmount => _expAmount;
}
