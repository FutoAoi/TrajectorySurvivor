using UnityEngine;

public class OrbitMoverState : IMoverState
{
    public float angle; // 現在の角度（度）
}

public class OrbitProjectileMover : IProjectileMover
{
    public Vector3 GetNextPosition(ProjectileMoveContext ctx)
    {
        var state = ctx.state as OrbitMoverState;
        if (state == null || ctx.owner == null) return ctx.currentPosition;

        state.angle += ctx.data.orbitAngularSpeed * ctx.deltaTime;
        float rad = state.angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * ctx.data.orbitRadius;
        return ctx.owner.position + offset;
    }
}