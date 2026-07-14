using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/MoveData")]
public class ProjectileMoveData : ScriptableObject
{
    public float speed = 10f;

    [Header("ジグザグ用")]
    public float zigzagFrequency = 4f;
    public float zigzagAmplitude = 1f;

    [Header("周回用")]
    public float orbitRadius = 2f;
    public float orbitAngularSpeed = 180f; // deg/sec

    [Header("ホーミング用")]
    public float homingTurnSpeed = 180f;   // deg/sec
    public float homingSearchRadius = 10f;

    [Header("ブーメラン用")]
    public float boomerangOutDistance = 5f;
    public float boomerangReturnSpeedMultiplier = 1.5f;

    [Header("スパイラル用")]
    public float spiralExpandSpeed = 1.5f; // 半径が広がる速さ
    public float spiralAngularSpeed = 360f;
}