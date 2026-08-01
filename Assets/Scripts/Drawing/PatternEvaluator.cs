using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatternEvaluator : MonoBehaviour
{
    [SerializeField] private Texture2D patternTexture;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Slider mistakeBar;
    [SerializeField] private Button NextButton;

    private bool[,] coveredPixel;
    private bool[,] wrongPixel;

    public int totalPatternPixel;
    public int coveredPatternPixel;
    public int mistakePixel;
        
    private void Start()
    {
        int width = patternTexture.width;
        int height = patternTexture.height;

        coveredPixel = new bool[width, height];
        wrongPixel = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (patternTexture.GetPixel(x, y).a > 0.1f)
                {
                    totalPatternPixel++;
                }
            }
        }

        if (progressBar != null)
        {
            progressBar.value = 0;

            TextMeshProUGUI progressBarLabel = progressBar.GetComponentInChildren<TextMeshProUGUI>();

            progressBarLabel.text = "0%";
        } 
        if (mistakeBar != null)
        {
            mistakeBar.value = 0;

            TextMeshProUGUI mistakeBarLabel = mistakeBar.GetComponentInChildren<TextMeshProUGUI>();

            mistakeBarLabel.text = "0%";
        }

        if (NextButton != null) NextButton.gameObject.SetActive(false);
    }

    public void CheckPixel(int x, int y)
    {
        int width = patternTexture.width;
        int height = patternTexture.height;

        // Ignore pixel di luar texture
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            return;
        }

        Color pattern = patternTexture.GetPixel(x, y);

        if (pattern.a > 0.1f)
        {
            if (!coveredPixel[x, y])
            {
                coveredPixel[x, y] = true;
                coveredPatternPixel++;
            }
        }
        else
        {
            if (!wrongPixel[x, y])
            {
                wrongPixel[x, y] = true;
                mistakePixel++;
            }
        }

        UpdateProgress();
    }

    private void UpdateProgress()
    {
        if (progressBar != null)
        {
            progressBar.value = GetCoverage();
            
            TextMeshProUGUI progressBarLabel = progressBar.GetComponentInChildren<TextMeshProUGUI>();

            progressBarLabel.text = $"{Mathf.RoundToInt(GetCoverage()*100)}%";
        }
        if (mistakeBar != null) mistakeBar.value = Mathf.Clamp01(((float)(mistakePixel - coveredPatternPixel) / totalPatternPixel));

        if (GetCoverage() >= 0.7f)
        {   
            if (NextButton != null) NextButton.gameObject.SetActive(true);
        }
    }

    public float GetCoverage()
    {
        return Mathf.Clamp01(((float)coveredPatternPixel / totalPatternPixel) * 1.2f);
    }

    public int GetMistake()
    {
        return mistakePixel;
    }

    public float GetScore()
    {
        float coverage =
            (float)coveredPatternPixel /
            totalPatternPixel;

        float penalty =
            (float)mistakePixel /
            totalPatternPixel;

        return Mathf.Clamp01(
            coverage - penalty
        );
    }
}
