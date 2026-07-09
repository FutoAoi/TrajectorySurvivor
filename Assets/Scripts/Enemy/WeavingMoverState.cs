using UnityEngine;

public class WeavingMoverState : IMoverState
{
    public float timeOffset;
}

public class WeavingChaseMover : IEnemyMover
{
    public MoveResult GetMove(EnemyMoveContext ctx)
    {
        var state = ctx.state as WeavingMoverState;
        if (state == null)
        {
            return new MoveResult { direction = Vector3.zero, speedMultiplier = 0f };
        }

        Vector3 toTarget = ctx.target.position - ctx.self.position;
        toTarget.y = 0;
        Vector3 forward = toTarget.normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        float wave = Mathf.Sin((Time.time + state.timeOffset) * ctx.data.waveFrequency) * ctx.data.waveAmplitude;

        Vector3 dir = (forward + right * wave * 0.1f).normalized;

        return new MoveResult
        {
            direction = dir,
            speedMultiplier = 1f // ‹È‚ª‚Á‚Ä‚¢‚Ä‚à‘¬“xŽ©‘Ì‚Í“™”{
        };
    }
}