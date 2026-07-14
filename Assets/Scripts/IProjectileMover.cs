using UnityEngine;

public interface IProjectileMover
{
    Vector3 GetNextPosition(ProjectileMoveContext ctx);
}

public struct ProjectileMoveContext
{
    public Vector3 currentPosition;
    public Vector3 originPosition;     // 発射地点（スポーン時固定）
    public Vector3 initialDirection;   // 発射時の方向
    public Transform owner;            // プレイヤー等、追従の起点にしたい対象
    public float speed;
    public float deltaTime;
    public float elapsedTime;
    public ProjectileMoveData data;
    public IMoverState state;          // 敵のMoverと同じ、動きごとの内部状態を入れる箱
}