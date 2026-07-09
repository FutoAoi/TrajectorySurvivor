using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/MoveData")]
public class EnemyMoveData : ScriptableObject
{
    public float moveSpeed = 3f;
    public float rotateSpeed = 360f;

    [Header("曲がる動き用")]
    public float waveFrequency = 2f;
    public float waveAmplitude = 1.5f;

    [Header("ダッシュ用")]
    public float dashSpeed = 10f;
    public float dashDuration = 0.3f;
    public float idleDuration = 1.5f;
}