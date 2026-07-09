using UnityEngine;

[CreateAssetMenu(menuName = "Spawn/SpawnWaveData")]
public class SpawnWaveData : ScriptableObject
{
    [System.Serializable]
    public class EnemyEntry
    {
        public GameObject enemyPrefab;
        [Range(0f, 1f)] public float weight = 1f; // 出現割合の重み
    }

    [System.Serializable]
    public class TimePhase
    {
        public string phaseName;           // Inspector上での識別用（例："0-60秒"）
        public float startTime;            // このフェーズが始まるゲーム内経過秒数
        public float spawnInterval;        // 何秒ごとに1体出すか
        public int spawnCountPerTick = 1;  // 1回のスポーンで何体出すか
        public EnemyEntry[] enemyPool;     // このフェーズで出現しうる敵とその重み
    }

    public TimePhase[] phases;

    public TimePhase GetPhaseAt(float elapsedTime)
    {
        TimePhase current = phases[0];
        foreach (var phase in phases)
        {
            if (elapsedTime >= phase.startTime)
                current = phase;
        }
        return current;
    }
}