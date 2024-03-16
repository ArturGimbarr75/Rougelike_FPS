using UnityEngine;

public class LerpRotation : MonoBehaviour
{
    [SerializeField] private bool _localSpace;
    [SerializeField] private Quaternion _startRotation;
    [SerializeField] private Quaternion _targetRotation;

#if UNITY_EDITOR
    [SerializeField, Range(0, 1)] private float _lerpValue;
	private float _previousLerpValue;
#endif

    public void SetLerpValue(float value)
    {
		_lerpValue = value;
		Quaternion rotation = Quaternion.Lerp(_startRotation, _targetRotation, _lerpValue);

		if (_localSpace)
			transform.localRotation = rotation;
		else
			transform.rotation = rotation;
	}

#if UNITY_EDITOR

	private void OnValidate()
	{
		if (_previousLerpValue == _lerpValue)
			return;

		_previousLerpValue = _lerpValue;
        SetLerpValue(_lerpValue);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(_startRotation * Vector3.forward, 0.1f);
		Gizmos.DrawSphere(_targetRotation * Vector3.forward, 0.1f);
		Gizmos.DrawLine(_startRotation * Vector3.forward, _targetRotation * Vector3.forward);
	}

	[ContextMenu("Set Current Rotation As Start")]
	private void SetCurretRotationAsStart()
	{
		_lerpValue = 0;
		_startRotation = _localSpace ? transform.localRotation : transform.rotation;
	}

	[ContextMenu("Set Current Rotation As Target")]
	private void SetCurretRotationAsTarget()
	{
		_lerpValue = 1;
		_targetRotation = _localSpace ? transform.localRotation : transform.rotation;
	}

#endif
}
