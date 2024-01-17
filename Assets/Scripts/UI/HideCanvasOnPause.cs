using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class HideCanvasOnPause : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

	private void Start()
	{
		_canvas.enabled = !PauseSystem.IsPaused;
	}

	private void OnEnable()
	{
		PauseSystem.Paused += OnGamePaused;
		PauseSystem.Unpaused += OnGameUnpaused;
	}

	private void OnDisable()
	{
		PauseSystem.Paused -= OnGamePaused;
		PauseSystem.Unpaused -= OnGameUnpaused;
	}

	private void OnGamePaused()
	{
		_canvas.enabled = false;
	}

	private void OnGameUnpaused()
	{
		_canvas.enabled = true;
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
    private void TryGetComponents()
    {
		_canvas ??= GetComponent<Canvas>();
	}

#endif
}
