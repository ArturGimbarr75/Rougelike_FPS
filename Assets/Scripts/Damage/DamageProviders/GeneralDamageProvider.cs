using UnityEngine;

public class GeneralDamageProvider : MonoBehaviour
{
    [field:SerializeField] public Health Health { get; private set; }
    
    public void ApplyDamage(Damage damage, Transform hitter)
    {
		Health.ApplyDamage(CalculateDamage(damage), hitter);
	}

    protected virtual float CalculateDamage(params Damage[] damage)
    {
        float totalDamage = 0;

        foreach (var d in damage)
			totalDamage += d.Value;

        return totalDamage;
    }
}
