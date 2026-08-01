using UnityEngine;
using UnityEngine.UI;

public class PaintTexture : MonoBehaviour
{
    [Header("Brush")]
    [SerializeField] private Color brushColor = Color.black;

    public int brushRadius = 5;

    public PatternEvaluator patternEvaluator;

    private Texture2D drawingTexture;

    private RawImage targetImage;

    private int width;
    private int height;

    public void Initialize(RawImage image, int textureWidth, int textureHeight)
    {
        targetImage = image;

        width = textureWidth;
        height = textureHeight;

        drawingTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        Clear();

        targetImage.texture = drawingTexture;
    }

    public void Clear()
    {
        Color32 background = new Color32(255, 238, 229, 255);

        Color[] colors = new Color[width * height];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = background;
        }

        drawingTexture.SetPixels(colors);
        drawingTexture.Apply();
    }

    public void PaintNormalized(float x, float y)
    {
        int pixelX = Mathf.RoundToInt(x * width);
        int pixelY = Mathf.RoundToInt(y * height);

        DrawCircle(pixelX, pixelY);

        drawingTexture.Apply();
    }

    private void DrawCircle(int centerX, int centerY)
    {
        for (int x = -brushRadius; x <= brushRadius; x++)
        {
            for (int y = -brushRadius; y <= brushRadius; y++)
            {
                if (x * x + y * y > brushRadius * brushRadius) continue;

                int px = centerX + x;
                int py = centerY + y;

                if (px < 0 || px >= width) continue;

                if (py < 0 || py >= height) continue;

                drawingTexture.SetPixel(px, py, brushColor);

                patternEvaluator.CheckPixel(px, py);
            }
        }
    }
}