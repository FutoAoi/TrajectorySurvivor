using UnityEngine;

public struct EnemyMoveContext
{
    public Transform self;
    public Transform target; // ƒvƒŒƒCƒ„[
    public Rigidbody rb;
    public float deltaTime;
    public EnemyMoveData data;
    public IMoverState state; // “®‚«‚²‚Æ‚Ì“à•”ó‘Ô‚ğ‚½‚¹‚é” 
}