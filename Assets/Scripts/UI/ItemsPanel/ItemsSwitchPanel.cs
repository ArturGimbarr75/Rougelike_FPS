using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsSwitchPanel : MonoBehaviour
{
    [SerializeField] private PlayerItemHolder _playerItemHolder;

    [Space(5)]
    [SerializeField, Range(0.5f, 5)] private float _showTime = 1f;
    [SerializeField] private Image _weel;
    [SerializeField] private Image _previous;
    [SerializeField] private Image _current;
    [SerializeField] private Image _next;

    [Space(5)]
    [SerializeField] private Animator _animator;

	private void Start()
	{
        HideWeel();
	}

	private void OnEnable()
	{
        _playerItemHolder.OnItemChangingToNext += OnItemChangingToNext;
        _playerItemHolder.OnItemChangingToPrevious += OnItemChangingToPrevious;
	}

    private void OnDisable()
    {
		_playerItemHolder.OnItemChangingToNext -= OnItemChangingToNext;
        _playerItemHolder.OnItemChangingToPrevious -= OnItemChangingToPrevious;
	}

    private void OnItemChangingToNext()
    {
        CancelInvoke(nameof(HideWeel));
        ShowWeel();
        SetImages();
        _animator.SetTrigger("Next");
        Invoke(nameof(HideWeel), _showTime);
	}

	private void OnItemChangingToPrevious()
	{
        CancelInvoke(nameof(HideWeel));
        ShowWeel();
        SetImages();
        _animator.SetTrigger("Previous");
        Invoke(nameof(HideWeel), _showTime);
	}

    private void SetImages()
    {
        _previous.sprite = _playerItemHolder.PreviousItem?.Icon;
        _current.sprite = _playerItemHolder.CurrentItem?.Icon;
        _next.sprite = _playerItemHolder.NextItem?.Icon;
    }

    public void ShowWeel()
    {
		_weel.gameObject.SetActive(true);
	}

    public void HideWeel()
    {
		_weel.gameObject.SetActive(false);
	}
}
