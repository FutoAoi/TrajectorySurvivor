using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileMoveController : MonoBehaviour, IPooledObject
{
    [Header("動きパターン設定")]
    [SerializeField] private ProjectileMoveType _moveType;
    [SerializeField] private ProjectileMoveData _data;

    [Header("コンポーネント参照")]
    [SerializeField] private Rigidbody _rb;

    private IProjectileMover _mover;
    private IMoverState _state;
    private Vector3 _originPosition;
    private Vector3 _initialDirection;
    private Transform _owner;
    private float _elapsedTime;

    /// <summary>
    /// 発射時にSkillManager等から呼ぶ
    /// </summary>
    public void Launch(Vector3 direction, Transform owner)
    {
        _initialDirection = direction;
        _owner = owner;
        _originPosition = transform.position;
        
    }

    public void OnSpawned()
    {
        (_mover, _state) = ProjectileMoverFactory.Create(_moveType);
        _elapsedTime = 0f;
    }

    public void OnDespawned() { }

    private void FixedUpdate()
    {
        if (GamePauseManager.IsPaused) return;
        if (_mover == null) return;

        _elapsedTime += Time.fixedDeltaTime;

        var ctx = new ProjectileMoveContext
        {
            currentPosition = _rb.position,
            originPosition = _originPosition,
            initialDirection = _initialDirection,
            owner = _owner,
            speed = _data.speed,
            deltaTime = Time.fixedDeltaTime,
            elapsedTime = _elapsedTime,
            data = _data,
            state = _state
        };

        Vector3 nextPos = _mover.GetNextPosition(ctx);
        _rb.MovePosition(nextPos);

        // 進行方向を向かせたい場合
        Vector3 moveDelta = nextPos - ctx.currentPosition;
        if (moveDelta.sqrMagnitude > 0.0001f)
        {
            _rb.MoveRotation(Quaternion.LookRotation(moveDelta.normalized));
        }
    }
}