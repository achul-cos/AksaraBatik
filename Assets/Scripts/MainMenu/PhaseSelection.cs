using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseSelection : MonoBehaviour
{
    public int PhaseIndex;

    public Vector3 originalScale;

    public void Start()
    {
        originalScale = gameObject.transform.localScale;
    }
}
