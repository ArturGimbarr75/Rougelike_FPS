using UnityEngine;

public abstract class HoldableItem : MonoBehaviour
{
    [field:SerializeField] public bool Droppable { get; private set; } = true;
    [field:SerializeField] public Vector3 HoldLocalPosition { get; private set; }
    [field:SerializeField] public Quaternion HoldLocalRotation { get; private set; }

    [field:Space]
    [field:SerializeField] public Sprite Icon { get; private set; }

    public abstract void OnPickup();
    public abstract void OnDrop();
    public abstract void OnShow();
    public abstract void OnHide();

#if UNITY_EDITOR

    [ContextMenu(nameof(SetCurrentPositionAsHoldPosition))]
    private void SetCurrentPositionAsHoldPosition()
    {
        HoldLocalPosition = transform.localPosition;
        HoldLocalRotation = transform.localRotation;
    }

#endif
}
