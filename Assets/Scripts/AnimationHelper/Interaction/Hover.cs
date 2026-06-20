using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scale = 1.1f;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = gameObject.GetComponent<Transform>().localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 originalScale = gameObject.GetComponent<Transform>().localScale;

        transform.DOScale(originalScale * scale, duration).SetEase(Ease.InOutQuart);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_originalScale, duration).SetEase(Ease.InOutQuart);
    }
}
