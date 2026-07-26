using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;

public class Kain : MonoBehaviour
{
    public bool OutlineEffect = true;
    public bool IsDraggable = true;
    public bool IsHover = true;
    public Fabric fabric;

    private DragableObject drag;
    private SpriteHoverOutline outline;
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = gameObject.transform.position;
        outline = gameObject.GetComponent<SpriteHoverOutline>() ?? gameObject.AddComponent<SpriteHoverOutline>();
        outline.hoverOutline = OutlineEffect;
        drag = gameObject.GetComponent<DragableObject>() ?? gameObject.AddComponent<DragableObject>();
        drag.isDragging = IsDraggable;
        drag.isHover = IsHover;
    }

    // Start is called before the first frame update
    void Start()
    {
        drag.onPointerEnterEvent.AddListener(MauDitarokKeTatakan);
        drag.onDragEvent.AddListener(LagiDigeser);
        drag.onEndDragEvent.AddListener(LagiDitaruh);
        drag.onPointerExitEvent.AddListener(GakJadiAmbil);
    }
    public void MauDitarokKeTatakan()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 10;
    }

    public void GakJadiAmbil()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 1;
    }

    public void LagiDigeser()
    {
        GameObject tatakan = FindAnyObjectByType<tatakan>().gameObject;

        if (tatakan != null)
        {
            float jarak = Vector3.Distance(tatakan.transform.position, gameObject.transform.position);

            if (jarak <= 4)
            {
                tatakan.transform.DOMove(tatakan.GetComponent<tatakan>().readyPostion, 0.5f).SetEase(Ease.OutQuint);
            }
            else if (jarak > 4)
            {
                if (Vector3.Distance(tatakan.transform.position, tatakan.GetComponent<tatakan>().idlePosition) >= 1f)
                {
                    tatakan.transform.DOMove(tatakan.GetComponent<tatakan>().idlePosition, 0.5f).SetEase(Ease.OutQuint);
                }
            }
        }
    }

    public void LagiDitaruh()
    {
        GameObject tatakan = FindAnyObjectByType<tatakan>().gameObject;

        if (tatakan != null)
        {
            float jarak = Vector3.Distance(tatakan.transform.position, gameObject.transform.position);

            if (jarak <= 4)
            {
                if (tatakan.GetComponent<tatakan>().kainTatakan != null)
                {
                    tatakan.GetComponent<tatakan>().kainTatakan.GetComponent<SpriteRenderer>().sprite = fabric.fabricSprite;

                    NextBatik();

                    CustomerManager.Instance.curretChooseFabric = fabric;

                    Destroy(gameObject);
                }
            }
            else
            {
                gameObject.transform.DOMove(initialPosition, 1.0f).SetEase(Ease.OutQuint);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        }
    }

    public void NextBatik()
    {
        GameObject tatakan = FindAnyObjectByType<tatakan>().gameObject;

        if (tatakan != null)
        {
            if (tatakan.GetComponent<tatakan>().kainTatakan.GetComponent<SpriteRenderer>().sprite != null)
            {
                tatakan.transform.DOMove(tatakan.GetComponent<tatakan>().initialPosition, 1.0f).SetEase(Ease.InQuint);

                GameManager.Instance.NextBatik();
            }
            else
            {
                return;
            }
        }
    }
}
