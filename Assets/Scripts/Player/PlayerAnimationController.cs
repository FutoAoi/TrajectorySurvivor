using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMoveController _playerMoveController;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private void Update()
    {
        _animator.SetFloat(SpeedHash, _playerMoveController.MoveSpeed());
    }
}
