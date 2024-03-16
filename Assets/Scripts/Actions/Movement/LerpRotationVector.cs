using UnityEngine;

public class LerpRotationVector : MonoBehaviour
{
    [SerializeField] private bool _localSpace;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Vector3 _targetRotation;

#if UNITY_EDITOR
    [SerializeField, Range(0, 1)] private float _lerpValue;
	private float _previousLerpValue;
#endif

    public void SetLerpValue(float value)
    {
		Vector3 vector = Vector3.LerpUnclamped(_startRotation, _targetRotation, value);
		Quaternion rotation = Quaternion.Euler(vector);

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

	[ContextMenu("Set Current Rotation As Start")]
	private void SetCurretRotationAsStart()
	{
		_lerpValue = 0;
		_previousLerpValue = 0;
		_startRotation = _localSpace
			? transform.localRotation.eulerAngles
			: transform.rotation.eulerAngles;
	}

	[ContextMenu("Set Current Rotation As Target")]
	private void SetCurretRotationAsTarget()
	{
		_lerpValue = 1;
		_previousLerpValue = 1;
		_targetRotation = _localSpace
			? transform.localRotation.eulerAngles
			: transform.rotation.eulerAngles;
	}

#endif
}
