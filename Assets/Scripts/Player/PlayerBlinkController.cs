using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlinkController : MonoBehaviour
{
    public int CurrentChages => _currentCharges;

    [Header("ダッシュ設定")]
    [SerializeField] private float _dashSpeed = 20f;
    [SerializeField] private float _dashDuration = 0.2f;

    [Header("チャージ設定")]
    [SerializeField] private int _maxCharges = 3;
    [SerializeField] private float _chargeRecoverTime = 3f; // 1チャージ回復にかかる時間

    [Header("コンポーネント参照")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private PlayerMoveController _moveController;
    [SerializeField] private HealthController _health;
    [SerializeField] private BlinkDamageDealer _dashDamageDealer;
    [SerializeField] private PlayerUIController _playerUIController;

    private int _currentCharges;
    private float _rechargeTimer;
    private bool _isBlinking;

    private void Awake()
    {
        _currentCharges = _maxCharges;
    }

    private void Update()
    {
        // チャージ回復（ダッシュ中かどうかに関係なく、最大値未満なら常に貯まる）
        if (_currentCharges < _maxCharges)
        {
            _rechargeTimer += Time.deltaTime;
            if (_rechargeTimer >= _chargeRecoverTime)
            {
                _rechargeTimer = 0f;
                _currentCharges++;
                _playerUIController.CurrentBlinkCharges = _currentCharges;
            }
        }
    }

    public void TryDash()
    {
        if (_isBlinking) return;
        if (_currentCharges <= 0) return;

        _currentCharges--;
        _playerUIController.CurrentBlinkCharges = _currentCharges;
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        _isBlinking = true;
        _moveController.IsBlinking = true;
        _health.SetInvincible(true);
        _dashDamageDealer.Activate();

        Vector3 dashDirection = _moveController.FacingDirection;
        float timer = 0f;


        while (timer < _dashDuration)
        {
            _rb.MovePosition(_rb.position + dashDirection * _dashSpeed * Time.fixedDeltaTime);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _rb.linearVelocity = Vector3.zero;

        _dashDamageDealer.Deactivate();
        _health.SetInvincible(false);
        _moveController.IsBlinking = false;
        _isBlinking = false;
    }
}