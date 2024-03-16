using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    [SerializeField] private bool _localSpace;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _targetPosition;

#if UNITY_EDITOR
    [SerializeField, Range(0, 1)] private float _lerpValue;
	private float _previousLerpValue;
#endif

    public void SetLerpValue(float value)
    {
		_lerpValue = value;
		Vector3 position = Vector3.Lerp(_startPosition, _targetPosition, _lerpValue);

		if (_localSpace)
            transform.localPosition = position;
        else
            transform.position = position;
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
		Gizmos.DrawSphere(_startPosition, 0.1f);
		Gizmos.DrawSphere(_targetPosition, 0.1f);
		Gizmos.DrawLine(_startPosition, _targetPosition);
	}

	[ContextMenu("Set Current Position As Start")]
	private void SetCurretPositionAsStart()
	{
		_lerpValue = 0;
		_startPosition = _localSpace ? transform.localPosition : transform.position;
	}

	[ContextMenu("Set Current Position As Target")]
	private void SetCurretPositionAsTarget()
	{
		_lerpValue = 1;
		_targetPosition = _localSpace ? transform.localPosition : transform.position;
	}

#endif
}
