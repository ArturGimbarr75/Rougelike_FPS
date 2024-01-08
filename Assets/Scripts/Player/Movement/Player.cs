using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	public static Player Instance { get; private set; }

    [field:SerializeField] public CharacterController CharacterController { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
	private void TryGetComponents()
	{
		if (CharacterController == null)
			CharacterController = GetComponent<CharacterController>();
	}

#endif
}
