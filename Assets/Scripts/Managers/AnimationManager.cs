using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationManager : Singleton<AnimationManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public Dictionary<GameObject, bool> IsClicked = new Dictionary<GameObject, bool>();

    // Hover Up Animation
    public void HoverUp(GameObject target, float move = 1.1f, float duration = 0.3f, Ease ease = Ease.InOutQuart)
    {
        target.GetComponent<RectTransform>().DOScale(target.GetComponent<RectTransform>().localScale * move, duration).SetEase(ease);
    }

    // Hover Down Animation
    public void HoverDown(GameObject target, Vector3 originalScale, float duration = 0.3f, Ease ease = Ease.InOutQuart)
    {
        target.GetComponent<RectTransform>().DOScale(originalScale, duration).SetEase(ease);
    }

    // Punch Animation
    public void PunchClick(GameObject target, float strength = -0.2f, float duration = 0.3f, Ease ease = Ease.OutCubic, int vibrato = 5, float elasticity = 0.2f, float cooldown = 0.5f)
    {
        if (!IsClicked.ContainsKey(target)) IsClicked.Add(target, false);

        if (IsClicked[target]) return;

        StartCoroutine(RoutinePunchClick(target, strength, duration, ease, vibrato, elasticity, cooldown));
    }

    public IEnumerator RoutinePunchClick(GameObject target, float strength, float duration, Ease ease, int vibrato, float elasticity, float cooldown)
    {
        IsClicked[target] = true;

        target.GetComponent<RectTransform>().DOPunchScale(target.GetComponent<RectTransform>().localScale * strength, duration, vibrato, elasticity).SetEase(ease);

        yield return new WaitForSeconds(cooldown);

        IsClicked[target] = false;
    }

    public void ClipOn(GameObject target, float duration = 0.3f, Ease ease = Ease.OutQuint)
    {
        Vector3 originalScale = target.GetComponent<RectTransform>().localScale;

        Vector3 newScale = target.GetComponent<RectTransform>().localScale;
        newScale.x = 0;
        target.GetComponent<RectTransform>().localScale = newScale;

        target.GetComponent<RectTransform>().DOScaleX(originalScale.x, duration).SetEase(ease);
    }

    public void ClipOut(GameObject target, float duration = 0.3f, Ease ease = Ease.OutQuint)
    {
        Vector3 originalScale = target.GetComponent<RectTransform>().localScale;

        target.GetComponent<RectTransform>().DOScaleX(0f, duration).SetEase(ease);

        StartCoroutine(HandleClipOut(target, duration, originalScale));
    }

    public IEnumerator HandleClipOut(GameObject target, float duration, Vector3 originalScale)
    {
        yield return new WaitForSeconds(duration);

        target.SetActive(false);

        target.GetComponent<RectTransform>().localScale = originalScale;
    }

    public void FadeIn(GameObject target, float duration = 0.3f, float opacity = 1.0f, Ease ease = Ease.InFlash)
    {
        target.SetActive(true);

        CanvasGroup cg = target.GetComponent<CanvasGroup>() ?? target.AddComponent<CanvasGroup>();

        cg.DOFade(opacity, duration).SetEase(ease);
    }

    public void FadeOut(GameObject target, float duration = 0.3f, float opacity = 0.0f, Ease ease = Ease.OutFlash, bool IsDisabled = true)
    {
        CanvasGroup cg = target.GetComponent<CanvasGroup>() ?? target.AddComponent<CanvasGroup>();

        float originalAlpha = cg.alpha;

        cg.DOFade(opacity, duration).SetEase(ease);

        if (IsDisabled) StartCoroutine(HandleFadeOut(target, duration, originalAlpha));
    }

    public IEnumerator HandleFadeOut(GameObject target, float duration, float originalAlpha)
    {
        yield return new WaitForSeconds(duration);

        target.SetActive(false);

        target.GetComponent<CanvasGroup>().alpha = originalAlpha;
    }
}
