public class HealthChangedEventArgs
{
	public float Delta { get; private set; }
	public float CurrentHealth => _health.Current;
	public float MaxHealth => _health.Max;
	public float Percent => _health.Percent;

	private Health _health;

	public HealthChangedEventArgs(float delta, Health health)
	{
		Delta = delta;
		_health = health;
	}
}
