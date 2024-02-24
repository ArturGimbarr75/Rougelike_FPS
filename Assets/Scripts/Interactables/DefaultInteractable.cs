using UnityEngine;
using UnityEngine.Events;

public class DefaultInteractable : MonoBehaviour, IInteractable
{
	public UnityEvent Interact;

	[field:SerializeField] public bool Active { get; private set; } = true;

	private void Awake()
	{
		Interact ??= new UnityEvent();
	}

	void IInteractable.Interact()
	{
		Interact?.Invoke();
	}
}
