using UnityEngine;
using UnityEngine.UI;

public class FullScreenHit : MonoBehaviour
{
    [SerializeField, Min(0)] private float _apearPercentPerDamageUnit;
    [SerializeField, Min(0)] private float _hideDuration;
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private AnimationCurve _alphaCurve;

	[SerializeField] private Image _image;
    [SerializeField] private Health _health;

	private float _currentPercent = 0;
	private Color _color = Color.white;

	private void Start()
	{
		SetScaleAndAlpha();
	}

	private void OnEnable()
	{
		_health.HealthReduced += OnDamaged;
	}

    private void OnDisable()
    {
		_health.HealthReduced -= OnDamaged;
	}

	private void Update()
	{
		if (_currentPercent <= 0)
			return;

		_currentPercent -= Time.deltaTime / _hideDuration;
		_currentPercent = Mathf.Clamp01(_currentPercent);

		SetScaleAndAlpha();
	}

	private void OnDamaged(object sender, HealthChangedEventArgs e)
    {
		if (e.Hitter is not null)
			return;

        _currentPercent += Mathf.Abs(e.Delta * _apearPercentPerDamageUnit);
		_currentPercent = Mathf.Clamp01(_currentPercent);
    }

	private void SetScaleAndAlpha()
	{
		float scale = _scaleCurve.Evaluate(_currentPercent);
		float alpha = _alphaCurve.Evaluate(_currentPercent);

		transform.localScale = Vector3.one * scale;
		_color.a = alpha;
		_image.color = _color;
	}
}
