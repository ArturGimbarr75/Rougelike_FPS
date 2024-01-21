using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void Start()
    {
		HidePanel();
	}

    private void OnEnable()
    {
        Player.Instance.Health.HealthEnd += OnEndHealth;
    }

    private void OnDisable()
    {
		Player.Instance.Health.HealthEnd -= OnEndHealth;
	}

    private void HidePanel()
    {
		_panel.SetActive(false);
	}

    private void ShowPanel()
    {
		_panel.SetActive(true);
	}

    private void OnEndHealth()
    {
        ShowPanel();
        PauseSystem.Pause();
    }

    #region Buttons

	public void OnRestartButtonClicked()
    {
		SceneSwitcher.RestartScene();
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
