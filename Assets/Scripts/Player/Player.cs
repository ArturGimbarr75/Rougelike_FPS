using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterControllerGravity))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(PlayerCrouch))]
public class Player : MonoBehaviour
{
	public static Player Instance { get; private set; }

	[field:SerializeField] public LayerMask PlayerLayerMask { get; private set; }

	[field:Header("Components")]
    [field:SerializeField] public CharacterController CharacterController { get; private set; }
	[field:SerializeField] public CharacterControllerGravity CharacterControllerGravity { get; private set; }
	[field:SerializeField] public PlayerJump PlayerJump { get; private set; }
	[field:SerializeField] public PlayerCrouch PlayerCrouch { get; private set; }
	[field:SerializeField] public PlayerView PlayerView { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
	private void TryGetComponents()
	{
		CharacterController ??= GetComponent<CharacterController>();
		CharacterControllerGravity ??= GetComponent<CharacterControllerGravity>();
		PlayerJump ??= GetComponent<PlayerJump>();
		PlayerCrouch ??= GetComponent<PlayerCrouch>();
		PlayerView ??= GetComponentInChildren<PlayerView>();
	}

#endif
}
