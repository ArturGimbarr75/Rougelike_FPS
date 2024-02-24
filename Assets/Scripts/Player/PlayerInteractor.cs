using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public event Action<IObjectWithInfo?> ObjectWithInfoChanged;
    public event Action<IInteractable?> InteractableChanged;
	public event Action<ILongInteractable?> LongInteractableChanged;

    [SerializeField, Range(0.1f, 5f)] private float _interactionDistance = 2f;

	private bool _isLongInteractableSaved = false;
    private IInteractable? _interactable;
	private bool? _interactableActive;
	private ILongInteractable? _longInteractable;
	private bool? _longInteractableActive;
    private IObjectWithInfo? _objectWithInfo;

	private Transform _camera;
	private LayerMask _playerLayerMask;

	private void Start()
	{
		_camera = Player.Instance.Camera.transform;
		_playerLayerMask = Player.Instance.PlayerLayerMask;
	}

	private void Update()
	{
        if (Input.GetKey(KeyCode.E))
        {
            if (_longInteractable is { Active: true })
				_longInteractable?.Interact();
			else if (Input.GetKeyDown(KeyCode.E) && _interactable is { Active: true })
				_interactable?.Interact();
        }
		else if (Input.GetKeyUp(KeyCode.E))
		{
			if (_longInteractable is { Active: true })
				_longInteractable?.StopInteraction();
		}
	}

	private void FixedUpdate()
    {
        Physics.Raycast(_camera.position,
						_camera.forward,
						out RaycastHit hit,
						_interactionDistance,
						~_playerLayerMask, // cast all layers except player layer
						QueryTriggerInteraction.Ignore);

		IObjectWithInfo? newObjectWithInfo = hit.collider?.GetComponent<IObjectWithInfo>();

		// Check if the object with info has changed
        if (newObjectWithInfo != _objectWithInfo)
        {
			_objectWithInfo = newObjectWithInfo;
			ObjectWithInfoChanged?.Invoke(_objectWithInfo);
		}

		IInteractable? newInteractable = (newObjectWithInfo as IInteractable)
										 ?? hit.collider?.GetComponent<IInteractable>();
		
		if (newInteractable is ILongInteractable newLongInteractable)
		{
			if (newLongInteractable != _longInteractable
				|| newLongInteractable.Active != _longInteractableActive
				|| !_isLongInteractableSaved)
			{
				_longInteractable = newLongInteractable;
				_longInteractableActive = newLongInteractable.Active;
				_isLongInteractableSaved = true;
				InteractableChanged?.Invoke(_longInteractable);
				LongInteractableChanged?.Invoke(_longInteractable);
			}
		}
		else
		{
			// If the new interactable is not a long interactable
			if (_isLongInteractableSaved)
			{
				_longInteractable?.StopInteraction();
				_longInteractable = null;
				_longInteractableActive = false;
				_isLongInteractableSaved = false;
				LongInteractableChanged?.Invoke(null);
			}

			if (newInteractable != _interactable
				|| _interactable?.Active != _interactableActive)
			{
				_interactable = newInteractable;
				_interactableActive = _interactable?.Active ?? false;
				InteractableChanged?.Invoke(_interactable);
				LongInteractableChanged?.Invoke(null);
			}
		}
    }
}
