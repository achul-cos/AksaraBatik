using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Konfigurasi suatu phase
/// </summary>
[System.Serializable]
public class PhaseConfig
{
    /// <summary>
    /// Nama phase/fase. Biasanya untuk text atau title.
    /// </summary>
    public string phaseName;

    /// <summary>
    /// Konfigurasi dari setiap days
    /// </summary>
    public DayConfig[] phaseDay;

    /// <summary>
    /// Berapa target pada value ini berdasarkan tipe data 
    /// </summary>
    public Target[] phaseTarget = new Target[]
    {
        new Target{targetType = TargetType.Customers, targetValue = 0},
        new Target{targetType = TargetType.Income, targetValue = 0}
    };

    /// <summary>
    /// Foto dari phase
    /// </summary>
    public Texture2D phaseImage;

    /// <summary>
    /// Deskripsi dari phase
    /// </summary>
    public string phaseDesc;
}
