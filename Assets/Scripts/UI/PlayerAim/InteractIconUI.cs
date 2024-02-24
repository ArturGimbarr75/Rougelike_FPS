using UnityEngine;
using UnityEngine.UI;

public class InteractIconUI : MonoBehaviour
{
	[SerializeField] private Image _interactIcon;
	[SerializeField] private Color _activeColor = Color.white;
	[SerializeField] private Color _inactiveColor = Color.gray;

	[Space(3)]
	[SerializeField] private PlayerInteractor _playerInteractor;

	private void Awake()
	{
		UpdateUI();
	}

	private void OnEnable()
	{
		if (_playerInteractor is not null)
			_playerInteractor.InteractableChanged += UpdateUI;
	}

	private void OnDisable()
	{
		if (_playerInteractor is not null)
			_playerInteractor.InteractableChanged -= UpdateUI;
	}

	private void UpdateUI(IInteractable? interactable = null)
	{
		_interactIcon.gameObject.SetActive(interactable is not null);
		_interactIcon.color = interactable?.Active ?? false? _activeColor : _inactiveColor;
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(SetParams))]
	private void SetParams()
	{
		_playerInteractor ??= GetComponentInParent<PlayerInteractor>();
		_interactIcon ??= transform.Find("InteractIconImage")?.GetComponent<Image>();
	}

	private void OnValidate()
	{
		if (_interactIcon is not null)
			_interactIcon.color = _activeColor;
	}

#endif
}
