using UnityEngine;

public class ZigzagMoverState : IMoverState
{
    public float distanceTraveled;
}

public class ZigzagProjectileMover : IProjectileMover
{
    public Vector3 GetNextPosition(ProjectileMoveContext ctx)
    {
        var state = ctx.state as ZigzagMoverState;
        if (state == null) return ctx.currentPosition;

        state.distanceTraveled += ctx.speed * ctx.deltaTime;

        Vector3 forward = ctx.initialDirection.normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        float lateral = Mathf.Sin(state.distanceTraveled * ctx.data.zigzagFrequency) * ctx.data.zigzagAmplitude;

        // 「進んだ距離」を基準に絶対座標を計算するので、フレームレートに関わらず滑らかな波形になる
        return ctx.originPosition + forward * state.distanceTraveled + right * lateral;
    }
}