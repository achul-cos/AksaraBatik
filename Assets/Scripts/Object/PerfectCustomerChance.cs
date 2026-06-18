using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Konfigurasi atau objek data yang mendefinisikan peluang kedatangan suatu customer berdasarkan tipe perfeksionismenya
/// </summary>
[System.Serializable]
public class PerfectCustomerChance
{
    [HideInInspector] public string perfectCustomerName;
    public PerfectType perfectType;
    [Range(0, 1)] public float perfectChance;
}
