using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PausableRigidbody : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _savedVelocity;
    private Vector3 _savedAngularVelocity;
    private bool _isFrozen;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        GamePauseManager.OnPauseChanged += HandlePauseChanged;

        // 生成された瞬間、既にポーズ中だった場合の対応
        if (GamePauseManager.IsPaused) HandlePauseChanged(true);
    }

    private void OnDisable()
    {
        GamePauseManager.OnPauseChanged -= HandlePauseChanged;
        _isFrozen = false;
    }

    private void HandlePauseChanged(bool paused)
    {
        if (paused)
        {
            _savedVelocity = _rb.linearVelocity;
            _savedAngularVelocity = _rb.angularVelocity;
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _isFrozen = true;
        }
        else if (_isFrozen)
        {
            _rb.linearVelocity = _savedVelocity;
            _rb.angularVelocity = _savedAngularVelocity;
            _isFrozen = false;
        }
    }
}