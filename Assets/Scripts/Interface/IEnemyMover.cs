using UnityEngine;

public interface IEnemyMover
{
    MoveResult GetMove(EnemyMoveContext ctx);
}

