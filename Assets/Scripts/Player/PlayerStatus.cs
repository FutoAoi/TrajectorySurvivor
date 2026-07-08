using UnityEngine;

public class PlayerStatus : MonoBehaviour, IHealthStatus
{
    [SerializeField] private int _playerMaxHealth;
    [SerializeField] private int _playerSpeed;
    [SerializeField] private int _playerRotateSpeed;
    [SerializeField] private int _PlayerDefense;

    public int MaxHealth => _playerMaxHealth;
    public int Defense => _PlayerDefense;
    public int PlayerSpeed => _playerSpeed;
    public int PlayerRotate => _playerRotateSpeed;

}
