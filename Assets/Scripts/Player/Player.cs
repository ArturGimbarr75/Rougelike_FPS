using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterControllerGravity))]
[RequireComponent(typeof(GeneralDamageProvider))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerCrouch))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
	public static Player Instance { get; private set; }

	public bool Alive => !Health.IsDead;

	public Vector3 LocalHead => Vector3.up * (CharacterController.height - Crouch.CameraOffset);
	public Vector3 GlobalHead => LocalHead + transform.position;
	public Vector3 LocalCenter => Vector3.up * (CharacterController.height / 2);
	public Vector3 GlobalCenter => LocalCenter + transform.position;

	[field:SerializeField] public LayerMask PlayerLayerMask { get; private set; }

	[field:Header("Components")]
    [field:SerializeField] public CharacterController CharacterController { get; private set; }
	[field:SerializeField] public CharacterControllerGravity GravityController { get; private set; }
	[field:SerializeField] public GeneralDamageProvider DamageProvider { get; private set; }
	[field:SerializeField] public Health Health { get; private set; }
	[field:SerializeField] public PlayerCrouch Crouch { get; private set; }
	[field:SerializeField] public PlayerJump Jump { get; private set; }
	[field:SerializeField] public PlayerMovement Movement { get; private set; }
	[field:SerializeField] public PlayerView View { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
	private void TryGetComponents()
	{
		CharacterController ??= GetComponent<CharacterController>();
		GravityController ??= GetComponent<CharacterControllerGravity>();
		DamageProvider ??= GetComponent<GeneralDamageProvider>();
		Health ??= GetComponent<Health>();
		Crouch ??= GetComponent<PlayerCrouch>();
		Jump ??= GetComponent<PlayerJump>();
		Movement ??= GetComponent<PlayerMovement>();
		View ??= GetComponentInChildren<PlayerView>();
	}

#endif
}
