using UnityEngine;
using UnityEngine.Events;

public class DefaultInteractable : MonoBehaviour, IInteractable
{
	public UnityEvent Interacted;

	[field:SerializeField] public bool Active { get; private set; } = true;

	private void Awake()
	{
		Interacted ??= new UnityEvent();
	}

	void IInteractable.Interact()
	{
		Interacted?.Invoke();
	}

	public void SetInteractableActive(bool active)
	{
		Active = active;
	}
}
