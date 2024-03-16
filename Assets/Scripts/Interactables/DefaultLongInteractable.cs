using System;
using UnityEngine;
using UnityEngine.Events;

public class DefaultLongInteractable : MonoBehaviour, ILongInteractable
{
	public UnityEvent Interacted;
	public UnityEvent InteractionDropped;
	[field: SerializeField] public UnityEvent<float> ProgressChangedUnityEvent { get; private set; }
	public event Action<float> ProgressChanged;

	[field: SerializeField, Range(0, 1)] public float Progress { get; protected set; } = 0f;
	[field: SerializeField] public bool Active { get; protected set; } = true;
	[field: SerializeField, Min(0.1f)] public float InteractionDuration { get; protected set; } = 2f;
	[field: SerializeField] public bool DropOnStop { get; protected set; } = true;

	[SerializeField] protected bool _resetProgressOnEnd = false;

	private void Awake()
	{
		Interacted ??= new UnityEvent();
		InteractionDropped ??= new UnityEvent();
		ProgressChangedUnityEvent ??= new UnityEvent<float>();
	}

	public virtual void Interact()
	{
		Progress += Time.deltaTime / InteractionDuration;
		Progress = Mathf.Clamp01(Progress);

		ProgressChanged?.Invoke(Progress);
		ProgressChangedUnityEvent?.Invoke(Progress);

		if (Progress >= 1f && _resetProgressOnEnd)
		{
			Interacted?.Invoke();
			ResetProgress();
		}
	}

	public void ResetProgress()
	{
		Progress = 0f;
		ProgressChanged?.Invoke(Progress);
		ProgressChangedUnityEvent?.Invoke(Progress);
	}

	public void StopInteraction()
	{
		if (DropOnStop)
		{
			ResetProgress();
			InteractionDropped?.Invoke();
		}
	}

	public void SetInteractableActive(bool active)
	{
		Active = active;
	}

	protected void InvokeProgressChanged()
	{
		ProgressChanged?.Invoke(Progress);
		ProgressChangedUnityEvent?.Invoke(Progress);
	}
}
