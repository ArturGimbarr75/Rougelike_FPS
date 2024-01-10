using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerGravity : MonoBehaviour
{
    public float VerticalVelocity { get; private set; } = 0f;
    public bool IsGrounded { get; private set; } = false;

    [Header("Params")]
    [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private float _minVerticalVelocity = -10f;

    [Header("Components")]
    [SerializeField] private CharacterController _characterController;

    private void FixedUpdate()
    {
		CollisionFlags collision = _characterController.Move(Vector3.up * VerticalVelocity * Time.fixedDeltaTime);
        IsGrounded = collision.HasFlag(CollisionFlags.Below);

		VerticalVelocity += Physics.gravity.y * _gravityScale * Time.fixedDeltaTime;
        VerticalVelocity = Mathf.Max(VerticalVelocity, _minVerticalVelocity);

        if (IsGrounded)
        {
            VerticalVelocity = 0f;
            transform.position = new Vector3(transform.position.x, _characterController.bounds.extents.y, transform.position.z);
        }
    }

    public void AddVelocity(float velocity)
    {
        VerticalVelocity += velocity;
    }

    public void SetVelocity(float velocity)
    {
		VerticalVelocity = velocity;
	}

#if UNITY_EDITOR

    [ContextMenu(nameof(TryGetComponents))]
    private void TryGetComponents()
    {
		_characterController ??= GetComponent<CharacterController>();
	}

#endif
}
