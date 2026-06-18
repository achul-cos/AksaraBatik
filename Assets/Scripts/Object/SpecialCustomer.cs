using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Konfigurasi atau objek data yang mendefinisikan suatu pelanggan spesial pada suatu hari yang dapat ditentukan siapa pelanggannya dan pada urutan apa dia hadir
/// </summary>
[System.Serializable]
public class SpecialCustomer
{
    //[HideInInspector] public string SpecialCustomerName => specialCustomer?.customerName ?? "Suatu customer special";
    [HideInInspector] public string specialCustomerName;
    public Customer specialCustomer;
    [Min(1)] public int specialCustomerIndex;
}