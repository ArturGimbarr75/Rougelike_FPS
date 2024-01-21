using UnityEngine;

public class HealthChangedEventArgs
{
	public float Delta { get; private set; }
	public float CurrentHealth => _health.Current;
	public float MaxHealth => _health.Max;
	public float Percent => _health.Percent;
	public Transform Hitter { get; private set; }

	private Health _health;

	public HealthChangedEventArgs(float delta, Health health, Transform hitter)
	{
		Delta = delta;
		_health = health;
		Hitter = hitter;
	}
}
