using System;

[Serializable]
public class Damage
{
    public DamageType Type { get; private set; }
    public float Value { get; private set; }

    public Damage(float value, DamageType type)
    {
		Value = value;
		Type = type;
	}
}
