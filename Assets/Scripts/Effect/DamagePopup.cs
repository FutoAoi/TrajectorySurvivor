using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class DamagePopup : MonoBehaviour, IPooledObject
{
    [Header("見た目設定")]
    [SerializeField] private float _lifeTime = 0.8f;
    [SerializeField] private float _moveUpSpeed = 1.5f;
    [SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.EaseInOut(0, 0.5f, 1, 1f);
    [SerializeField] private AnimationCurve _alphaCurve = AnimationCurve.Linear(0, 1, 1, 0);

    [Header("色設定")]
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _critColor = Color.yellow;

    [Header("コンポーネント")]
    [SerializeField] private TextMeshPro _text;
    private float _timer;
    private int _damage;
    private bool _isCrit;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    public void SetData(int damage, bool isCrit)
    {
        _damage = damage;
        _isCrit = isCrit;
        _timer = 0f;
        _text.text = _damage.ToString();
        _text.color = _isCrit ? _critColor : _normalColor;
        transform.localScale = Vector3.one * _scaleCurve.Evaluate(0);
    }

    public void OnSpawned()
    {
        //_timer = 0f;
        //_text.text = _damage.ToString();
        //_text.color = _isCrit ? _critColor : _normalColor;
        //transform.localScale = Vector3.one * _scaleCurve.Evaluate(0);
    }

    public void OnDespawned() { }

    private void Update()
    {
        _timer += Time.deltaTime;
        float t = _timer / _lifeTime;

        transform.position += Vector3.up * _moveUpSpeed * Time.deltaTime;
        transform.localScale = Vector3.one * _scaleCurve.Evaluate(t);

        var color = _text.color;
        color.a = _alphaCurve.Evaluate(t);
        _text.color = color;

        if (t >= 1f)
        {
            PoolManager.Instance.Return(gameObject);
        }
    }
}