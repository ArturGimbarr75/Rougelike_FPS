using UnityEngine;

public class FallDamage : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damagePerVelocity = 10f;
    [SerializeField, Min(0)] private float _minVelocity = 10f;

    [SerializeField] private CharacterControllerGravity _gravity;
    [SerializeField] private GeneralDamageProvider _damageProvider;

    private void OnEnable()
    {
		_gravity.Land += OnLand;
	}

    private void OnDisable()
    {
		_gravity.Land -= OnLand;
	}

	private void OnLand(object sender, LandEventArgs args)
    {
        float velocity = Mathf.Abs(args.Velocity);
		if (velocity < _minVelocity)
			return;

        float damageValue = (velocity - _minVelocity) * _damagePerVelocity;
        Damage damage = new(damageValue, DamageType.Fall);
        _damageProvider.ApplyDamage(damage, null);
	}

#if UNITY_EDITOR

    [ContextMenu(nameof(TryGetComponents))]
    private void TryGetComponents()
    {
        _gravity ??= GetComponent<CharacterControllerGravity>();
		_damageProvider ??= GetComponent<GeneralDamageProvider>();
    }

#endif
}
