using UnityEngine;

public enum EnemyMoveType { Straight, Weaving, StopAndDash }

public static class EnemyMoverFactory
{
    public static (IEnemyMover mover, IMoverState state) Create(EnemyMoveType type)
    {
        return type switch
        {
            EnemyMoveType.Straight => (new StraightChaseMover(), null),
            EnemyMoveType.Weaving => (new WeavingChaseMover(), new WeavingMoverState { timeOffset = Random.Range(0f, 10f) }),
            EnemyMoveType.StopAndDash => (new StopAndDashMover(), new StopAndDashMoverState()),
            _ => (new StraightChaseMover(), null)
        };
    }
}