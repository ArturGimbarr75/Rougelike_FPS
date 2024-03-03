using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHolder : MonoBehaviour
{
    public event Action<HoldableItem> OnItemAdded;
    public event Action<HoldableItem> OnItemRemoved;
    public event Action<HoldableItem?> OnItemChanged;

    public int ItemsCount => _items.Count;
    public HoldableItem? CurrentItem
        => ItemsCount > 0 ? _items[_currentItemIndex] : null;
    public HoldableItem? NextItem
        => ItemsCount > 0 ? _items[(_currentItemIndex + 1) % ItemsCount] : null;
    public HoldableItem? PreviousItem 
        => ItemsCount > 0 ? _items[(_currentItemIndex - 1 + ItemsCount) % ItemsCount] : null;

    private List<HoldableItem> _items = new();
    private int _currentItemIndex = 0;

    public HoldableItem this[int index] => _items[index];

    [SerializeField, Min(0)] private float _throwImpulse = 7f;
    [SerializeField, Min(0)] private float _throwTorque = 13f;

    private void Start()
    {
        foreach (var item in GetComponentsInChildren<HoldableItem>(true))
            AddItem(item);
    }

    private void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            if (scroll > 0)
                SwitchToNextItem();
            else
                SwitchToPreviousItem();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (CurrentItem?.Droppable ?? false)
                RemoveItem(CurrentItem);
        }
    }

    private void SwitchToNextItem()
    {
        if (_items.Count < 2)
            return;

        CurrentItem.OnHide();
        CurrentItem.gameObject.SetActive(false);
        _currentItemIndex = (_currentItemIndex + 1) % _items.Count;
        CurrentItem.gameObject.SetActive(true);
        CurrentItem.OnShow();

        OnItemChanged?.Invoke(CurrentItem);
    }

    private void SwitchToPreviousItem()
    {
        if (_items.Count < 2)
            return;

        CurrentItem.OnHide();
        CurrentItem.gameObject.SetActive(false);
        _currentItemIndex = (_currentItemIndex + _items.Count - 1) % _items.Count;
        CurrentItem.gameObject.SetActive(true);
        CurrentItem.OnShow();

        OnItemChanged?.Invoke(CurrentItem);
    }

    public void AddItem(HoldableItem item)
    {
        if (_items.Contains(item))
            return;

        _items.Add(item);

        item.OnPickup();
        item.gameObject.SetActive(false);
        item.transform.parent = transform;
        item.OnHide();

        OnItemAdded?.Invoke(item);

        if (_items.Count == 1)
        {
            _currentItemIndex = 0;
            CurrentItem.gameObject.SetActive(true);
            CurrentItem.OnShow();
        }
    }

    public void RemoveItem(HoldableItem item)
    {
        if (_items.Contains(item))
        {
            item.transform.parent = null;
            _items.Remove(item);
            item.gameObject.SetActive(true);
            item.OnDrop();

            if (item.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(Camera.main.transform.forward * _throwImpulse, ForceMode.Impulse);
                rb.AddTorque(UnityEngine.Random.insideUnitSphere * _throwTorque, ForceMode.Impulse);
            }

            if (_items.Count == 0)
            {
                _currentItemIndex = 0;
                OnItemChanged?.Invoke(null);
            }
            else
            {
                _currentItemIndex %= _items.Count;
                CurrentItem.gameObject.SetActive(true);
                CurrentItem.OnShow();
                OnItemChanged?.Invoke(CurrentItem);
            }

            OnItemRemoved?.Invoke(item);
        }
    }
}
