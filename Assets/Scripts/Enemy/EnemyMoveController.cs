using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMoveController : MonoBehaviour ,IPooledObject
{
    [Header("動きパターン設定")]
    [SerializeField] private EnemyMoveType _moveType;
    [SerializeField] private EnemyMoveData _data;

    [Header("コンポーネント参照")]
    [SerializeField] private Rigidbody _rb;

    private Transform _target;
    private IEnemyMover _mover;
    private IMoverState _state;

    private void Awake()
    {
        (_mover, _state) = EnemyMoverFactory.Create(_moveType);
    }

    private void Start()
    {
        // プレイヤー参照は実行時に解決（シーンに1人という前提。複数対応なら別途注入方式に）
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) _target = player.transform;
    }

    private void FixedUpdate()
    {
        if (_mover == null || _target == null) return;

        var ctx = new EnemyMoveContext
        {
            self = transform,
            target = _target,
            rb = _rb,
            deltaTime = Time.fixedDeltaTime,
            data = _data,
            state = _state
        };

        MoveResult result = _mover.GetMove(ctx);

        _rb.linearVelocity = result.direction * _data.moveSpeed * result.speedMultiplier;

        if (result.direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(result.direction);
            _rb.MoveRotation(Quaternion.RotateTowards(_rb.rotation, targetRot, _data.rotateSpeed * Time.fixedDeltaTime));
        }
    }

    public void OnSpawned()
    {
        (_mover, _state) = EnemyMoverFactory.Create(_moveType);
    }

    public void OnDespawned() { }
}