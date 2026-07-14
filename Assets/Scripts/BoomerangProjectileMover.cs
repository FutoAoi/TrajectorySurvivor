using UnityEngine;

public enum BoomerangPhase { Outbound, Returning }

public class BoomerangMoverState : IMoverState
{
    public BoomerangPhase phase = BoomerangPhase.Outbound;
}

public class BoomerangProjectileMover : IProjectileMover
{
    public Vector3 GetNextPosition(ProjectileMoveContext ctx)
    {
        var state = ctx.state as BoomerangMoverState;
        if (state == null || ctx.owner == null) return ctx.currentPosition;

        if (state.phase == BoomerangPhase.Outbound)
        {
            float distFromOrigin = Vector3.Distance(ctx.originPosition, ctx.currentPosition);
            if (distFromOrigin >= ctx.data.boomerangOutDistance)
            {
                state.phase = BoomerangPhase.Returning;
            }
            return ctx.currentPosition + ctx.initialDirection.normalized * ctx.speed * ctx.deltaTime;
        }
        else
        {
            Vector3 dirToOwner = (ctx.owner.position - ctx.currentPosition).normalized;
            float returnSpeed = ctx.speed * ctx.data.boomerangReturnSpeedMultiplier;
            return ctx.currentPosition + dirToOwner * returnSpeed * ctx.deltaTime;
        }
    }
}