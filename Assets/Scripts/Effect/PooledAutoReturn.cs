using UnityEngine;

public class PooledAutoReturn : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _lifeTime = 1f;

    private float _timer;
    private ParticleSystem _ps;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    public void OnSpawned()
    {
        _timer = 0f;
        _ps.Clear();
        _ps.Play();
    }

    public void OnDespawned()
    {
        _ps.Stop();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _lifeTime)
        {
            PoolManager.Instance.Return(gameObject);
        }
    }
}