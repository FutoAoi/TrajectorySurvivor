using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour
{
    public float MoveSpeed () => _inputValue.magnitude;

    [Header("コンポーネント設定")]
    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private PlayerBlinkController _dashController;

    private Vector3 _moveDirection;
    private Vector2 _inputValue;
    private Quaternion _targetRotate;
    private Quaternion _newRotate;

    public bool IsBlinking { get; set; }
    public Vector3 FacingDirection => _moveDirection.sqrMagnitude > 0.001f
        ? _moveDirection.normalized
        : transform.forward;

    private void OnMove(InputValue inputValue)
    {
        _inputValue = inputValue.Get<Vector2>();
    }

    private void OnInteract()
    {
        _dashController.TryDash();
    }

    private void FixedUpdate()
    {
        if (IsBlinking) return;
        PlayerMove();
        PlayerRotation();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void PlayerMove()
    {
        _moveDirection = new Vector3(_inputValue.x , 0, _inputValue.y);
        _rb.linearVelocity = _moveDirection  * _playerStatus.PlayerSpeed;
    }


    /// <summary>
    /// 回転処理
    /// </summary>
    private void PlayerRotation()
    {
        if (_inputValue.sqrMagnitude < 0.001) return;
        _targetRotate = Quaternion.LookRotation(_moveDirection);

        _newRotate = Quaternion.RotateTowards(_rb.rotation, _targetRotate, _playerStatus.PlayerRotate * Time.deltaTime);

        _rb.MoveRotation(_newRotate);
    }
}
