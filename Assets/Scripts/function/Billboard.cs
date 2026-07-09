using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _cam;

    private void OnEnable()
    {
        if (_cam == null) _cam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.rotation = _cam.transform.rotation;
    }
}

