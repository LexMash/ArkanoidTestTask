using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Camera _targetCamera;
    [SerializeField] private Vector2 _defaultResolution = new Vector2(720, 1280);

    private float _initialSize;
    private float _targetAspect;

    private void Start()
    {
        _initialSize = _targetCamera.orthographicSize;
        _targetAspect = _defaultResolution.x / _defaultResolution.y;

        float constantWidthSize = _initialSize * (_targetAspect / _targetCamera.aspect);
        _targetCamera.orthographicSize = constantWidthSize;
    }
}