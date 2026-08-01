using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhaseSelection : MonoBehaviour, IPointerClickHandler
{
    public int PhaseIndex;

    public Vector3 originalScale;

    public GameObject selection;

    public MainMenuUIManager mainMenuUIManager;

    private void Awake()
    {
        originalScale = transform.localScale;

        mainMenuUIManager = FindAnyObjectByType<MainMenuUIManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mainMenuUIManager.SetPhaseSelection(PhaseIndex);
    }
}
