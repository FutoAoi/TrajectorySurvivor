using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [System.Serializable]
    public class PoolPreset
    {
        public GameObject prefab;
        public int initialSize = 10;
    }

    [SerializeField] private List<PoolPreset> _presets = new();

    private readonly Dictionary<GameObject, Queue<GameObject>> _pools = new();
    private readonly Dictionary<GameObject, GameObject> _instanceToPrefab = new(); // 返却時に元プレハブを逆引き
    private readonly Dictionary<GameObject, Transform> _poolParents = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        foreach (var preset in _presets)
        {
            CreatePool(preset.prefab, preset.initialSize);
        }
    }

    private Queue<GameObject> CreatePool(GameObject prefab, int initialSize)
    {
        var queue = new Queue<GameObject>();

        var parent = new GameObject($"Pool_{prefab.name}").transform;
        parent.SetParent(transform);

        _pools[prefab] = queue;
        _poolParents[prefab] = parent;

        for (int i = 0; i < initialSize; i++)
        {
            queue.Enqueue(CreateInstance(prefab, parent));
        }

        return queue;
    }

    private GameObject CreateInstance(GameObject prefab, Transform parent)
    {
        var obj = Instantiate(prefab, parent);
        obj.SetActive(false);
        _instanceToPrefab[obj] = prefab;
        return obj;
    }

    public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!_pools.TryGetValue(prefab, out var queue))
        {
            queue = CreatePool(prefab, 0); // 未登録プレハブは初回アクセス時に自動生成
        }

        GameObject obj = queue.Count > 0
            ? queue.Dequeue()
            : CreateInstance(prefab, _poolParents[prefab]);

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        if (obj.TryGetComponent<IPooledObject>(out var pooled))
            pooled.OnSpawned();

        return obj;
    }

    public void Return(GameObject instance)
    {
        if (!_instanceToPrefab.TryGetValue(instance, out var prefab))
        {
            Destroy(instance); // プール管理外のオブジェクトは通常のDestroy
            return;
        }

        if (instance.TryGetComponent<IPooledObject>(out var pooled))
            pooled.OnDespawned();

        instance.SetActive(false);
        instance.transform.SetParent(_poolParents[prefab]);
        _pools[prefab].Enqueue(instance);
    }
}