using UnityEngine;
using UnityEngine.UI;

public class ItemsPanel : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Sprite _defautIcon;
    [SerializeField] private PlayerItemHolder _playerItemHolder;

	private void OnEnable()
	{
		_playerItemHolder.OnItemChanged += OnItemChanged;
	}

	private void OnDisable()
	{
		_playerItemHolder.OnItemChanged -= OnItemChanged;
	}

	private void OnItemChanged(HoldableItem? item)
	{
		_itemImage.sprite = item?.Icon ?? _defautIcon;
	}
}
