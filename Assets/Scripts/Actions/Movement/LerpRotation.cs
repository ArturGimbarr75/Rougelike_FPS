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
		Quaternion rotation = Quaternion.Slerp(_startRotation, _targetRotation, value);

		if (_localSpace)
			transform.localRotation = rotation;
		else
			transform.rotation = rotation;
	}

#if UNITY_EDITOR

	private void OnValidate()
	{
		if (_lerpValue != _previousLerpValue)
		{
			SetLerpValue(_lerpValue);
			_previousLerpValue = _lerpValue;
		}
	}

	[ContextMenu("Set Current Rotation As Start")]
	private void SetCurretRotationAsStart()
	{
		_lerpValue = 0;
		_previousLerpValue = 0;
		_startRotation = _localSpace ? transform.localRotation : transform.rotation;
	}

	[ContextMenu("Set Current Rotation As Target")]
	private void SetCurretRotationAsTarget()
	{
		_lerpValue = 1;
		_previousLerpValue = 1;
		_targetRotation = _localSpace ? transform.localRotation : transform.rotation;
	}

#endif
}
