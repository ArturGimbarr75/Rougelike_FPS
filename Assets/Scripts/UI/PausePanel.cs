using UnityEngine;
using UnityEngine.SceneManagement;

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
		PauseSystem.Paused += OnPaused;
		PauseSystem.Unpaused += HidePanel;
	}

	private void OnDisable()
	{
		PauseSystem.Paused -= OnPaused;
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

	private void OnPaused()
	{
		if (Player.Instance.Alive)
			ShowPanel();
	}

	#region Buttons

	public void OnResumeButtonClicked()
	{
		PauseSystem.Unpause();
	}

	public void OnRestartButtonClicked()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void OnSettingsButtonClicked()
	{
		Debug.Log("Settings button clicked"); // TODO: Open settings panel
	}

	public void OnQuitButtonClicked()
	{
		SceneSwitcher.LoadMainMenu();
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
