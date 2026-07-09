using UnityEngine;

public class StraightChaseMover : IEnemyMover
{
    public MoveResult GetMove(EnemyMoveContext ctx)
    {
        Vector3 dir = ctx.target.position - ctx.self.position;
        dir.y = 0;

        return new MoveResult
        {
            direction = dir.normalized,
            speedMultiplier = 1f // èÌÇ…í èÌë¨ìx
        };
    }
}