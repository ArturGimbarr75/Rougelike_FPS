using System;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSourcePointersPool : MonoBehaviour
{
    [SerializeField] private ImpactSourcePointer _hitFromHitterPrefab;
	[SerializeField] private Health _health;

    private Stack<ImpactSourcePointer> _hitsPool;
    private List<ImpactSourcePointer> _hitsInUse;

    private void Awake()
    {
		_hitsPool = new (GetComponentsInChildren<ImpactSourcePointer>());
        _hitsInUse = new();
	}

	private void OnEnable()
	{
		_health.HealthReduced += OnHealthReduced;
	}

	private void OnDisable()
	{
		_health.HealthReduced -= OnHealthReduced;
	}

	private void OnHealthReduced(object sender, HealthChangedEventArgs e)
	{
		if (e.Hitter is null)
			return;

		foreach (ImpactSourcePointer hitInUse in _hitsInUse)
		{
			if (hitInUse.Hitter == e.Hitter)
			{
				hitInUse.ResetPercent();
				return;
			}
		}

		ImpactSourcePointer hit = _hitsPool.Count > 0 ? _hitsPool.Pop() : Instantiate(_hitFromHitterPrefab, transform);

		hit.Show(e.Hitter);
		hit.Disappear += OnHitDisappear;
		_hitsInUse.Add(hit);
	}

	private void OnHitDisappear(object sender, EventArgs e)
	{
		if (sender is ImpactSourcePointer hit)
		{
			hit.Disappear -= OnHitDisappear;
			_hitsInUse.Remove(hit);
			_hitsPool.Push(hit);
		}
	}
}
