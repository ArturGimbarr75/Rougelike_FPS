using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

	private void Start()
	{
		if (PauseSystem.IsPaused)
			ShowPanel();
		else
			HidePanel();
	}

	private void OnEnable()
    {
		PauseSystem.Paused += ShowPanel;
		PauseSystem.Unpaused += HidePanel;
	}

	private void OnDisable()
	{
		PauseSystem.Paused -= ShowPanel;
		PauseSystem.Unpaused -= HidePanel;
	}

	private void HidePanel()
	{
		_panel.SetActive(false);
	}

	private void ShowPanel()
	{
		_panel.SetActive(true);
	}

	#region Buttons

	public void OnResumeButtonClicked()
	{
		PauseSystem.Unpause();
	}

	public void OnSettingsButtonClicked()
	{
		Debug.Log("Settings button clicked"); // TODO: Open settings panel
	}

	public void OnQuitButtonClicked()
	{
		Debug.Log("Quit button clicked"); // TODO: Back to main menu
	}

	#endregion Buttons

#if UNITY_EDITOR

	[ContextMenu(nameof(GetPanel))]
	private void GetPanel()
	{
		_panel = transform.GetChild(0).gameObject;
	}

#endif
}
