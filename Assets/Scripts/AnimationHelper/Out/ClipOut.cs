using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClipOut : MonoBehaviour
{
    [SerializeField] private float duration = 0.3f;

    private Vector2 originalSize;

    public void Awake()
    {
        originalSize = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        gameObject.SetActive(true);

        gameObject.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0f, 0f), duration).SetEase(Ease.InOutQuart);

        gameObject.SetActive(false);
    }
}
