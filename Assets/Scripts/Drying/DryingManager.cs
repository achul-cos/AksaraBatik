using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DryingManager : MonoBehaviour
{
    [Header("Object")]
    public SpriteRenderer batikJemur;

    [Header("UI")]
    public Slider jemurSlider;
    public Button MulaiButton;
    public Button NextButton;

    [Header("Setting")]
    public float duration = 10f;

    private Tween dryingTween;
    private TextMeshProUGUI sliderLabel;

    private void Start()
    {
        batikJemur.color = new Color32(172, 176, 202, 255);

        jemurSlider.minValue = 0;
        jemurSlider.maxValue = 1;
        jemurSlider.value = 0;

        sliderLabel = jemurSlider.GetComponentInChildren<TextMeshProUGUI>();

        if (sliderLabel != null)
            sliderLabel.text = "0%";

        NextButton.gameObject.SetActive(false);
        MulaiButton.gameObject.SetActive(true);

        MulaiButton.onClick.AddListener(StartDrying);
        NextButton.onClick.AddListener(NextResult);
    }

    private void StartDrying()
    {
        // Hilangkan tombol mulai
        MulaiButton.gameObject.SetActive(false);

        // Animasi warna kain
        dryingTween = batikJemur.DOColor(Color.white, duration);

        // Animasi slider
        DOVirtual.Float(0, 1, duration, value =>
        {
            jemurSlider.value = value;

            if (sliderLabel != null)
            {
                sliderLabel.text = $"{Mathf.RoundToInt(value * 100)}%";
            }
        })
        .OnComplete(() =>
        {
            jemurSlider.value = 1;

            if (sliderLabel != null)
                sliderLabel.text = "100%";

            NextButton.gameObject.SetActive(true);
        });
    }

    private void NextResult()
    {
        GameManager.Instance.NextResult();
    }

    private void OnDestroy()
    {
        dryingTween?.Kill();
    }
}