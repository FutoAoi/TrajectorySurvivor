using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [Header("プレイヤーからの距離")]
    [SerializeField] private Vector3 _offset = new Vector3(0f, 10f, -8f);

    [Header("追従速度")]
    [SerializeField] private float _followSpeed = 8f;

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPos = _target.position + _offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            _followSpeed * Time.deltaTime);
    }
}
