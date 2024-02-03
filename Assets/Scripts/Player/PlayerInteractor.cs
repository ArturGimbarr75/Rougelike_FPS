using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public event Action<IObjectWithInfo?> ObjectWithInfoChanged;

    [SerializeField, Range(0.1f, 5f)] private float _interactionDistance = 2f;

    private IInteractable? _interactable;
    private IObjectWithInfo? _objectWithInfo;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
			_interactable?.Interact();
	}

	private void FixedUpdate()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _interactionDistance);

		IObjectWithInfo? newObjectWithInfo = hit.collider?.GetComponent<IObjectWithInfo>();
        IInteractable? newInteractable = newObjectWithInfo as IInteractable;

        if (newObjectWithInfo != _objectWithInfo)
        {
			_objectWithInfo = newObjectWithInfo;
			ObjectWithInfoChanged?.Invoke(_objectWithInfo);
		}
    }
}
