using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public event Action<IObjectWithInfo?> ObjectWithInfoChanged;
    public event Action<IInteractable?> InteractableChanged;

    [SerializeField, Range(0.1f, 5f)] private float _interactionDistance = 2f;

    private IInteractable? _interactable;
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
		if (Input.GetKeyDown(KeyCode.E))
			_interactable?.Interact();
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
		IInteractable? newInteractable = (newObjectWithInfo as IInteractable)
										 ?? hit.collider?.GetComponent<IInteractable>();

        if (newObjectWithInfo != _objectWithInfo)
        {
			_objectWithInfo = newObjectWithInfo;
			ObjectWithInfoChanged?.Invoke(_objectWithInfo);
		}

		if (newInteractable != _interactable)
		{
			_interactable = newInteractable;
			InteractableChanged?.Invoke(_interactable);
		}
    }
}
