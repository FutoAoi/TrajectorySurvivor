using UnityEngine;

public interface IHealthStatus
{
    int MaxHealth { get; }
    int Defense { get; }
    Faction Faction { get; }
}
