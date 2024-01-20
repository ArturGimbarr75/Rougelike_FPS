using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    [Space(10)]
    [SerializeField] private Image _fill;
    [SerializeField] private bool _useGradient;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private TMP_Text _text;
    [Tooltip("{0} - current health\n" +
             "{1} - max health\n" +
             "{2} - current health in percent 0-100%\n" +
             "{3} - current health in percent 0-1")]
    [SerializeField] private string _format = "{0:0} / {1:0}";

	private void Start()
	{
		UpdateHealthBar(this, new HealthChangedEventArgs(0, _health, null));
	}

	private void OnEnable()
	{
		_health.HealthChanged += UpdateHealthBar;
	}

    private void OnDisable()
    {
		_health.HealthChanged -= UpdateHealthBar;
	}

    private void UpdateHealthBar(object sender, HealthChangedEventArgs e)
    {
		if (_useGradient)
			_fill.color = _gradient.Evaluate(_health.Percent);

		_fill.fillAmount = _health.Percent;

        if (_text is not null)
		    _text.text = string.Format(_format, _health.Current, _health.Max, _health.Percent * 100, _health.Percent);
	}
}
