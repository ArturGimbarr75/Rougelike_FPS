using System;
using UnityEngine;
using UnityEngine.Events;

public class DefaultLongInteractable : MonoBehaviour, ILongInteractable
{
	public UnityEvent Interacted;
	public UnityEvent InteractionDropped;
	public event Action<float> ProgressChanged;

	public float Progress { get; private set; } = 0f;
	[field: SerializeField] public bool Active { get; private set; } = true;
	[field: SerializeField, Min(0.1f)] public float InteractionDuration { get; private set; } = 2f;
	[field: SerializeField] public bool DropOnStop { get; private set; } = true;

	private void Awake()
	{
		Interacted ??= new UnityEvent();
		InteractionDropped ??= new UnityEvent();
	}

	public void Interact()
	{
		Progress += Time.deltaTime / InteractionDuration;
		Progress = Mathf.Clamp01(Progress);

		ProgressChanged?.Invoke(Progress);

		if (Progress >= 1f)
		{
			Interacted?.Invoke();
			Progress = 0f;
		}
	}

	public void StopInteraction()
	{
		if (DropOnStop)
		{
			Progress = 0f;
			ProgressChanged?.Invoke(Progress);
			InteractionDropped?.Invoke();
		}
	}

	public void SetInteractableActive(bool active)
	{
		Active = active;
	}
}
