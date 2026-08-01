using UnityEngine;
using UnityEngine.UI;

public class DrawingCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private RawImage drawingImage;

    [Header("Painter")]
    [SerializeField] private PaintTexture painter;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = drawingImage.rectTransform;

        painter.Initialize(drawingImage, 512, 512);
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;

        Vector2 localPoint;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform,Input.mousePosition, null, out localPoint)) return;

        Rect rect = rectTransform.rect;

        float x = Mathf.InverseLerp(rect.xMin, rect.xMax, localPoint.x);
        float y = Mathf.InverseLerp(rect.yMin, rect.yMax, localPoint.y);

        if (FindAnyObjectByType<DrawingManager>().currentCanting != null) painter.PaintNormalized(x, y);
    }
}