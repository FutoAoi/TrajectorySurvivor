using UnityEngine;

public class PlayerStatus : MonoBehaviour, IHealthStatus
{
    [SerializeField] private int _playerMaxHealth;
    [SerializeField] private int _playerSpeed;
    [SerializeField] private int _playerRotateSpeed;
    [SerializeField] private int _playerDefense;
    [SerializeField] private Faction _faction;
    [SerializeField] private int _playerMaxBlinkPoint;

    public int MaxHealth => _playerMaxHealth;
    public int Defense => _playerDefense;
    public int PlayerSpeed => _playerSpeed;
    public int PlayerRotate => _playerRotateSpeed;
    public Faction Faction => _faction;
}
