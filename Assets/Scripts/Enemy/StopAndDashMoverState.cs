using UnityEngine;

public enum DashPhase { Idle, Dashing }

public class StopAndDashMoverState : IMoverState
{
    public DashPhase phase = DashPhase.Idle;
    public float timer;
    public Vector3 dashDirection;
}

public class StopAndDashMover : IEnemyMover
{
    public MoveResult GetMove(EnemyMoveContext ctx)
    {
        var state = ctx.state as StopAndDashMoverState;
        if (state == null) return default;

        state.timer += ctx.deltaTime;

        switch (state.phase)
        {
            case DashPhase.Idle:
                if (state.timer >= ctx.data.idleDuration)
                {
                    Vector3 dir = ctx.target.position - ctx.self.position;
                    dir.y = 0;
                    state.dashDirection = dir.normalized;
                    state.phase = DashPhase.Dashing;
                    state.timer = 0f;
                }
                return new MoveResult { direction = Vector3.zero, speedMultiplier = 0f };

            case DashPhase.Dashing:
                if (state.timer >= ctx.data.dashDuration)
                {
                    state.phase = DashPhase.Idle;
                    state.timer = 0f;
                    return new MoveResult { direction = Vector3.zero, speedMultiplier = 0f };
                }
                return new MoveResult
                {
                    direction = state.dashDirection,
                    speedMultiplier = ctx.data.dashSpeed / ctx.data.moveSpeed // baseSpeed‚Ö‚Ì”{—¦‰»
                };
        }

        return default;
    }
}