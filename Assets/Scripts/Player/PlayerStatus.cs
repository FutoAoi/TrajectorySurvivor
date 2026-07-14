using System.Collections.Generic;
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

    private readonly List<StatModifier> _modifiers = new();

    public void AddModifier(StatModifier modifier) => _modifiers.Add(modifier);

    public void RemoveModifiersFromSource(object source)
    {
        _modifiers.RemoveAll(m => m.source == source);
    }

    public float GetFinalValue(StatType stat, float baseValue)
    {
        float additive = 0f;
        float multiplicative = 1f;

        foreach (var m in _modifiers)
        {
            if (m.stat != stat) continue;
            if (m.mode == StatModifierMode.Additive) additive += m.value;
            else multiplicative += m.value;
        }

        return (baseValue + additive) * multiplicative;
    }
}
