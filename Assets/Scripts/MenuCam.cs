using UnityEngine;
using Random = UnityEngine.Random;

public class MenuCam : MonoBehaviour
{
    [SerializeField] private float shakeAmount; //The amount to shake this frame.
    [SerializeField] private float smoothAmount = 5f; //Amount to smooth
    [SerializeField] private float timer = 1;

    private Vector3 _defaultRotation = Vector3.zero;
    private Vector3 _targetRotation = Vector3.zero;
    private float _currentTime = 0;

    private void Start()
    {
        _defaultRotation = transform.localEulerAngles;
        _targetRotation = SetTargetRotation(_defaultRotation, shakeAmount);
        _currentTime = timer;
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetRotation),
            Time.deltaTime * smoothAmount / timer);

        _currentTime -= Time.deltaTime;
        if (_currentTime > 0) return;
        _currentTime = timer;
        _targetRotation = SetTargetRotation(_defaultRotation, smoothAmount);
    }

    private static Vector3 SetTargetRotation(Vector3 defaultRotation, float amount)
    {
        Vector3 rotationAmount = Random.insideUnitSphere * amount;
        rotationAmount.z = 0;

        rotationAmount += defaultRotation;
        return rotationAmount;
    }
}