using UnityEngine;

public class GrabInteraction : MonoBehaviour, IInteractable
{
    [field:SerializeField] public bool Active { get; private set; } = true;

    [SerializeField] private HoldableItem _item;

    public void Interact()
    {
        Player.Instance.ItemHolder.AddItem(_item);
    }
}
