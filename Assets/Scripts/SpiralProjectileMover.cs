using UnityEngine;

public class SpiralMoverState : IMoverState
{
    public float angle;
    public float currentRadius;
}

public class SpiralProjectileMover : IProjectileMover
{
    public Vector3 GetNextPosition(ProjectileMoveContext ctx)
    {
        var state = ctx.state as SpiralMoverState;
        if (state == null) return ctx.currentPosition;

        state.angle += ctx.data.spiralAngularSpeed * ctx.deltaTime;
        state.currentRadius += ctx.data.spiralExpandSpeed * ctx.deltaTime;

        Vector3 forward = ctx.initialDirection.normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        float forwardDistance = ctx.speed * ctx.elapsedTime;
        float rad = state.angle * Mathf.Deg2Rad;
        Vector3 spiralOffset = right * Mathf.Cos(rad) * state.currentRadius
                              + Vector3.up * Mathf.Sin(rad) * state.currentRadius;

        return ctx.originPosition + forward * forwardDistance + spiralOffset;
    }
}