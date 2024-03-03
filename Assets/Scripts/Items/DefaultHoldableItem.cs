using UnityEngine;

public class DefaultHoldableItem : HoldableItem
{
    [SerializeField] private Behaviour[] _enableOnDropDisableOnPickup;

    private Rigidbody _rigidbody;
    private Collider[] _colliders;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>(true);
    }

    public override void OnDrop()
    {
        _rigidbody.isKinematic = false;
        foreach (var collider in _colliders)
            collider.enabled = true;

        foreach (var component in _enableOnDropDisableOnPickup)
            component.enabled = true;
    }

    public override void OnHide()
    {

    }

    public override void OnPickup()
    {
        _rigidbody.isKinematic = true;
        foreach (var collider in _colliders)
            collider.enabled = false;

        foreach (var component in _enableOnDropDisableOnPickup)
            component.enabled = false;
    }

    public override void OnShow()
    {
        transform.localPosition = HoldLocalPosition;
        transform.localRotation = HoldLocalRotation;
    }
}
