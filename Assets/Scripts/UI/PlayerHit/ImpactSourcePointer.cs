using System;
using UnityEngine;
using UnityEngine.UI;

public class ImpactSourcePointer : MonoBehaviour
{
	public event EventHandler Disappear;

	public Transform Hitter { get; private set; }

	[SerializeField] private float _hideDuration;
	[SerializeField] private AnimationCurve _scaleCurve;
	[SerializeField] private AnimationCurve _alphaCurve;

	[SerializeField] private Image _image;
	[SerializeField] private RectTransform _rectTransform;

    private Transform _hitter;
	private float _currentPercent = 1;
	private Color _color = Color.white;

	public void Show(Transform hitter)
	{
		Hitter = hitter;
		_hitter = hitter;
		gameObject.SetActive(true);
		ResetPercent();
	}

	public void ResetPercent()
	{
		_currentPercent = 1;
		_color.a = _alphaCurve.Evaluate(_currentPercent);
		_image.color = _color;
		_rectTransform.localScale = Vector3.one * _scaleCurve.Evaluate(_currentPercent);
	}

	private void LateUpdate()
	{
		if (_hitter is null)
			return;

		// Hide
		_currentPercent -= Time.deltaTime / _hideDuration;
		_currentPercent = Mathf.Clamp01(_currentPercent);

		_color.a = _alphaCurve.Evaluate(_currentPercent);
		_image.color = _color;
		_rectTransform.localScale = Vector3.one * _scaleCurve.Evaluate(_currentPercent);

		if (_currentPercent <= 0)
		{
			Disappear?.Invoke(this, EventArgs.Empty);
			gameObject.SetActive(false);
			return;
		}

		// Rotate to hitter
		Vector3 playerForward = Player.Instance.transform.forward;
		Vector3 toHitter = _hitter.position - Player.Instance.transform.position;
		toHitter.y = 0;

		float angle = Vector3.SignedAngle(toHitter, playerForward, Vector3.up);
		transform.rotation = Quaternion.Euler(Vector3.forward * angle);
	}
}
