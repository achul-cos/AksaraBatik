using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tatakan : MonoBehaviour
{
    public GameObject kainTatakan;
    public Vector3 initialPosition;
    public Vector3 idlePosition;
    public Vector3 readyPostion;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Start()
    {
        gameObject.transform.DOMove(idlePosition, 2.0f).SetEase(Ease.InOutElastic);
    }
}
