using UnityEngine;

public class ResettableLongInteraction : DefaultLongInteractable
{
	[SerializeField] private bool _reverseProgress = false;

	private int _lastFrame = 0;

	public override void Interact()
	{
		if (Progress >= 1f && !_reverseProgress
			|| Progress <= 0f && _reverseProgress)
			return;

		if (_reverseProgress)
			Progress -= Time.deltaTime / InteractionDuration;
		else
			Progress += Time.deltaTime / InteractionDuration;
		Progress = Mathf.Clamp01(Progress);

		InvokeProgressChanged();
		ProgressChangedUnityEvent?.Invoke(Progress);

		if (_reverseProgress && Progress <= 0f
			|| !_reverseProgress && Progress >= 1f)
			Interacted?.Invoke();
	}

	public void SetProgress(float progress)
	{
		// prevent setting progress multiple times in the same frame
		if (_lastFrame == Time.frameCount)
			return;

		_lastFrame = Time.frameCount;
		progress = Mathf.Clamp01(progress);

		if (Mathf.Approximately(Progress, progress))
			return;

		Progress = progress;
		InvokeProgressChanged();
	}

	public void SetProgressInversed(float progress)
	{
		progress = Mathf.Clamp01(progress);
		progress = 1 - progress;

		SetProgress(progress);
	}

#if UNITY_EDITOR

	private void OnValidate()
	{
		DropOnStop = false;
		_resetProgressOnEnd = false;
	}

#endif
}
