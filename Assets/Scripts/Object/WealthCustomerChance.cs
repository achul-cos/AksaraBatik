using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Konfigurasi atau objek data yang mendefinisikan peluang kedatangan suatu customer bedasarkan tipe kekayaannya
/// </summary>
[System.Serializable]
public class WealthCustomerChance
{
    [HideInInspector] public string wealthName;
    public WealthType wealthType;
    [Range(0, 1)] public float wealthChance;
}
