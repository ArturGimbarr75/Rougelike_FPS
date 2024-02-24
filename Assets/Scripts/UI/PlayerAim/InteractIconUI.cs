using UnityEngine;
using UnityEngine.UI;

public class InteractIconUI : MonoBehaviour
{
	[SerializeField] private GameObject _interactIcon;

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
		_interactIcon.SetActive(interactable is not null);
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(SetParams))]
	private void SetParams()
	{
		_playerInteractor ??= GetComponentInParent<PlayerInteractor>();
		_interactIcon ??= transform.Find("InteractIconImage").gameObject;
	}

#endif
}
