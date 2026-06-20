using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClipIn : MonoBehaviour
{
    [SerializeField] private float duration = 0.3f;

    private Vector2 originalSize;

    private void Awake()
    {
        originalSize = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);

        gameObject.GetComponent<RectTransform>().DOSizeDelta(originalSize, duration).SetEase(Ease.InOutQuart);
    }
}
