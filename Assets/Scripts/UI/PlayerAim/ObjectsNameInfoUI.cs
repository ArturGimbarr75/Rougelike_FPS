using TMPro;
using UnityEngine;

public class ObjectsNameInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _infoText;

    [Space(3)]
    [SerializeField] private PlayerInteractor _playerInteractor;

	private void Awake()
	{
		UpdateUI();
	}

	private void OnEnable()
    {
        if (_playerInteractor is not null)
		    _playerInteractor.ObjectWithInfoChanged += UpdateUI;
	}

    private void OnDisable()
    {
		if (_playerInteractor is not null)
			_playerInteractor.ObjectWithInfoChanged -= UpdateUI;
	}

	private void UpdateUI(IObjectWithInfo? obj = null)
    {
		_nameText.text = obj?.Name ?? string.Empty;
		_infoText.text = obj?.Info ?? string.Empty;
	}

#if UNITY_EDITOR

    [ContextMenu(nameof(SetParams))]
    private void SetParams()
    {
		_playerInteractor ??= GetComponentInParent<PlayerInteractor>();
        _nameText ??= transform.Find("ObjectNameText")?.GetComponent<TMP_Text>();
        _infoText ??= transform.Find("ObjectInfoText")?.GetComponent<TMP_Text>();
	}

#endif
}
