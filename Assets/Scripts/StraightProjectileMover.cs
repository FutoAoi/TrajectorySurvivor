using UnityEngine;

public class StraightProjectileMover : IProjectileMover
{
    public Vector3 GetNextPosition(ProjectileMoveContext ctx)
    {
        return ctx.currentPosition + ctx.initialDirection.normalized * ctx.speed * ctx.deltaTime;
    }
}