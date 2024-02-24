using UnityEngine;

public class ObjectWithInfo : MonoBehaviour, IObjectWithInfo
{
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public string Info { get; private set; }
}
