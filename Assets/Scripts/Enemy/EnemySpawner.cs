using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnWaveData _waveData;
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _offScreenMargin = 2f;

    private float _elapsedTime;
    private float _spawnTimer;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _spawnTimer += Time.deltaTime;

        var phase = _waveData.GetPhaseAt(_elapsedTime);

        if (_spawnTimer >= phase.spawnInterval)
        {
            _spawnTimer = 0f;
            SpawnEnemies(phase);
        }
    }

    private void SpawnEnemies(SpawnWaveData.TimePhase phase)
    {
        for (int i = 0; i < phase.spawnCountPerTick; i++)
        {
            GameObject prefab = EnemyEntrySelector.Select(phase.enemyPool);
            Vector3 pos = SpawnPositionCalculator.GetOffScreenPosition(_player, _mainCamera, _offScreenMargin);

            PoolManager.Instance.Get(prefab, pos, Quaternion.identity);
        }
    }
}