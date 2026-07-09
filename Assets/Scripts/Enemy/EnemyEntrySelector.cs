using UnityEngine;

public static class EnemyEntrySelector
{
    public static GameObject Select(SpawnWaveData.EnemyEntry[] pool)
    {
        float totalWeight = 0f;
        foreach (var entry in pool) totalWeight += entry.weight;

        float rand = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var entry in pool)
        {
            cumulative += entry.weight;
            if (rand <= cumulative) return entry.enemyPrefab;
        }

        return pool[pool.Length - 1].enemyPrefab; // フェイルセーフ
    }
}