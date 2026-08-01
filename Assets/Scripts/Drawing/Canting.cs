using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Canting : MonoBehaviour, IPointerClickHandler
{
    public int brushSize = 5;

    public Transform ujungCanting;

    public Vector2 originalPosition;

    public Vector2 CantingOffset = new Vector2(-500f, -200f);

    public void Start()
    {
        ujungCanting = transform.GetComponentInChildren<Transform>();

        originalPosition = transform.localPosition;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        DrawingManager manager = FindAnyObjectByType<DrawingManager>();

        if (manager != null)
        {
            manager.SetCanting(this);
        }
    }
}
