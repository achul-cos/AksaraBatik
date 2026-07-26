using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragableObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Camera mainCamera;
    private Vector3 offset;

    public UnityEvent onEndDragEvent = new UnityEvent();
    public UnityEvent onPointerEnterEvent = new UnityEvent();
    public UnityEvent onPointerExitEvent = new UnityEvent();
    public UnityEvent onDragEvent = new UnityEvent();
    public bool isDragging = true;
    public bool isHover = true;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHover)
        {
            // Jalankan semua event yang subs
            if (onPointerEnterEvent != null)
            {
                onPointerEnterEvent?.Invoke();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHover)
        {
            if (onPointerExitEvent != null)
            {
                onPointerExitEvent?.Invoke();
            }
        }
    }

    // Fungsi yang jalan tepat saat objek pertama kali diklik & digeser
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Hitung selisih posisi antara titik klik mouse dengan pusat objek
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(eventData.position);
            mousePosition.z = transform.position.z; // Kunci koordinat Z
            offset = transform.position - mousePosition;
        }

    }

    // Fungsi yang jalan terus-menerus selama mouse digeser (di-hold)
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(eventData.position);
            mousePosition.z = transform.position.z; // Kunci koordinat Z

            // Pindahkan posisi objek mengikuti mouse + offset agar tidak "lompat" ke tengah
            transform.position = mousePosition + offset;

            if (onDragEvent != null)
            {
                onDragEvent?.Invoke();
            }
        }

    }

    // Fungsi yang jalan saat klik mouse dilepas
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Anda bisa memasukkan logika di sini (misal: efek jatuh, sound effect, dll)
            // Debug.Log("Objek selesai di-drag!");

            // Nanti ada fungsi snapping disini

            if (onEndDragEvent != null)
            {
                onEndDragEvent?.Invoke();
            }   
        }

    }
}
