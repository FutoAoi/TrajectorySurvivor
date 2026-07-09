using UnityEngine;

public class EnemyStatus : MonoBehaviour, IHealthStatus
{
    [SerializeField] private int _enemyMaxHealth;
    [SerializeField] private int _enemyDefense;
    [SerializeField] private Faction _faction;
    
    public int MaxHealth => _enemyMaxHealth;
    public int Defense => _enemyDefense;

    public Faction Faction => _faction;
}
