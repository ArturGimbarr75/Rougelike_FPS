using UnityEngine;
using UnityEngine.UI;

public class LongInteractUI : MonoBehaviour
{
	[SerializeField] private Image _sliderBackground;
	[SerializeField] private Image _sliderFill;

	[Space(3)]
	[SerializeField] private PlayerInteractor _playerInteractor;

	private ILongInteractable? _longInteractable;

	private void Awake()
	{

		UpdateUI(0);
	}

	private void OnEnable()
	{
		if (_playerInteractor is not null)
			_playerInteractor.LongInteractableChanged += OnLongInteractableChanged;
	}

	private void OnDisable()
	{
		if (_playerInteractor is not null)
			_playerInteractor.LongInteractableChanged -= OnLongInteractableChanged;
	}

	private void OnLongInteractableChanged(ILongInteractable? longInteractable = null)
	{
		if (_longInteractable is not null)
			_longInteractable.ProgressChanged -= UpdateUI;

		_longInteractable = longInteractable;
		_sliderBackground.gameObject.SetActive(_longInteractable is not null);

		if (_longInteractable is not null)
			_longInteractable.ProgressChanged += UpdateUI;

		UpdateUI(_longInteractable?.Progress ?? 0);
	}

	private void UpdateUI(float fill)
	{
		_sliderFill.fillAmount = fill;
	}
}
