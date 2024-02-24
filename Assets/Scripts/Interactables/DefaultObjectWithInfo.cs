using UnityEngine;

public class DefaultObjectWithInfo : MonoBehaviour, IObjectWithInfo
{
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public string Info { get; private set; }
}
