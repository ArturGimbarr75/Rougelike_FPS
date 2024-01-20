using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
	[SerializeField, Min(0)] private float _damagePerSecond = 1f;
	[SerializeField] private DamageType _damageType;

    private HashSet<GeneralDamageProvider> _damageProviders = new();

	private void OnTriggerEnter(Collider other)
	{
		GeneralDamageProvider damageProvider = other.GetComponent<GeneralDamageProvider>();
		if (damageProvider is not null)
			_damageProviders.Add(damageProvider);
	}

	private void OnTriggerExit(Collider other)
	{
		GeneralDamageProvider damageProvider = other.GetComponent<GeneralDamageProvider>();
		if (damageProvider is not null)
			_damageProviders.Remove(damageProvider);
	}

	private void LateUpdate()
	{
		if (_damageProviders.Count <= 0)
			return;

		Damage damage = new(_damagePerSecond * Time.deltaTime, _damageType);
		foreach (var damageProvider in _damageProviders)
			damageProvider.ApplyDamage(damage, null);
	}
}
