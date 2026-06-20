using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PunchClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float scale = -0.2f;
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private int vibrato = 5;
    [SerializeField] private float elasticity = 0.5f;
    [SerializeField] private float cooldown = 0.25f;

    private Vector3 _originalScale;
    private bool isCooldown = false;

    private void Awake()
    {
        _originalScale = gameObject.GetComponent<Transform>().localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCooldown) return; 

        StartCoroutine(HandlePointerClick());
    }

    private IEnumerator HandlePointerClick()
    {
        isCooldown = true;

        transform.DOPunchScale(_originalScale * scale, duration, vibrato, elasticity);

        yield return new WaitForSeconds(cooldown);

        isCooldown = false;
    }
}
