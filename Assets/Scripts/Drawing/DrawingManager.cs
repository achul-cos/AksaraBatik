using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class DrawingManager : MonoBehaviour
{
    public Canting currentCanting;

    public RectTransform cantingRect;
    public RectTransform canvasRect;
    public PaintTexture paintTexture;

    public Canting[] cantings;

    [SerializeField] private Vector2 CantingOffset = new Vector2(400f, 0f);

    public Button NextButton;

    public void Start()
    {
        canvasRect = FindAnyObjectByType<Canvas>().GetComponent<RectTransform>();

        cantings = FindObjectsByType<Canting>(FindObjectsSortMode.None);

        NextButton.onClick.AddListener(NextBoiling);
    }

    public void NextBoiling()
    {
        GameManager.Instance.NextRebus();
    }

    public void UnsetCanting()
    {
        if (cantings  != null)
        {
            foreach (Canting canting in cantings)
            {
                canting.transform.localPosition = canting.originalPosition;
            }
        }
    }

    public void SetCanting(Canting canting)
    {
        UnsetCanting();

        currentCanting = canting;
        cantingRect = currentCanting.GetComponent<RectTransform>();

        paintTexture.brushRadius = currentCanting.brushSize;
    }

    public void Update()
    {
        if (currentCanting != null) MoveCanting();
    }

    public void MoveCanting()
    {
        Vector2 mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out mousePosition);

        if (currentCanting.CantingOffset != null) cantingRect.localPosition = mousePosition - currentCanting.CantingOffset;
        else cantingRect.localPosition = mousePosition - CantingOffset;
    }
}
