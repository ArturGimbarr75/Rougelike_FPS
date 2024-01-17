using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field:SerializeField] public float Current { get; private set; }
    [field:SerializeField] public float Max { get; private set; }

    public bool IsDead => Current <= 0;
    public float Percent => Current / Max;

    public event Action HealthFull;
    public event EventHandler<HealthChangedEventArgs> HealthChanged;
    public event EventHandler<HealthChangedEventArgs> HealthIncreased;
    public event EventHandler<HealthChangedEventArgs> HealthReduced;
    public event Action HealthEnd;
	public event EventHandler<HealthChangedEventArgs> MaxChanged;

    public float ApplyDamage(float damage)
    {
        if (damage <= 0)
            return 0;

		float delta = Math.Min(Current, damage);
		Current -= delta;
        HealthChangedEventArgs args = new (-delta, this);
		HealthChanged?.Invoke(this, args);
        HealthReduced?.Invoke(this, args);

		if (IsDead)
			HealthEnd?.Invoke();

        return delta;
	}

	public float ApplyHeal(float heal)
    {
		if (heal <= 0)
			return 0;

        float delta = Math.Min(Max - Current, heal);
        Current += delta;
        HealthChangedEventArgs args = new (delta, this);
        HealthChanged?.Invoke(this, args);
        HealthIncreased?.Invoke(this, args);

        if (Current >= Max)
			HealthFull?.Invoke();

        return delta;
    }

    public float FullHeal()
    {
        if (Current == Max)
            return 0;

        float delta = Max - Current;
		Current = Max;
		HealthChangedEventArgs args = new (delta, this);
		HealthChanged?.Invoke(this, args);
		HealthIncreased?.Invoke(this, args);
		HealthFull?.Invoke();

        return delta;
	}

	public float Kill()
    {
		if (IsDead)
			return 0;

		float delta = Current;
		Current = 0;
		HealthChangedEventArgs args = new (-delta, this);
		HealthChanged?.Invoke(this, args);
		HealthReduced?.Invoke(this, args);
		HealthEnd?.Invoke();

		return delta;
	}

    public void SetMax(float max, CurrentHealthChangeType changeType = CurrentHealthChangeType.IncreaseByDelta)
    {
		if (Current == Max)
			return;

		float maxDelta = max - Max;
		float currentDelta = 0;
		Max = max;

		if (changeType.HasFlag(CurrentHealthChangeType.ClampNotIncrease))
		{
			if (Current > Max)
			{
				currentDelta = Max - Current;
				Current = Max;
			}
		}
		else if (changeType.HasFlag(CurrentHealthChangeType.ChangeToMax))
		{
			currentDelta = Max - Current;
			Current = Max;
		}
		else
		{
			if (currentDelta < 0 && changeType.HasFlag(CurrentHealthChangeType.DecreaseByDelta))
			{
				Current += maxDelta;
				currentDelta = maxDelta;
			}
			
			if (currentDelta > 0 && changeType.HasFlag(CurrentHealthChangeType.IncreaseByDelta))
			{
				Current += maxDelta;
				currentDelta = maxDelta;
			}
		}

		HealthChangedEventArgs args = new (currentDelta, this);
		HealthChanged?.Invoke(this, args);
		MaxChanged?.Invoke(this, args);
	}

	public enum CurrentHealthChangeType
	{
		ClampNotIncrease = 0,
		ChangeToMax = 1,
		IncreaseByDelta = 2,
		DecreaseByDelta = 4,
	}
}
