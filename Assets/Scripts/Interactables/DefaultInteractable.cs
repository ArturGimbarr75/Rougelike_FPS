using UnityEngine;
using UnityEngine.Events;

public class DefaultInteractable : MonoBehaviour, IInteractable
{
	public UnityEvent Interact;

	private void Awake()
	{
		Interact ??= new UnityEvent();
	}

	void IInteractable.Interact()
	{
		Interact?.Invoke();
	}
}
