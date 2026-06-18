using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class yang miliki properti dictionary yang mengonversi tipe wealthType dengan nilai multiplier
/// </summary>
public static class Wealth
{
    private static readonly Dictionary<WealthType, float> _wealthDict = new Dictionary<WealthType, float>()
    {
        {WealthType.MISKIN, 0.8f },
        {WealthType.BIASA,  1.0f},
        {WealthType.KAYA, 1.5f },
        {WealthType.SULTAN, 2.0f }
    };

    public static Dictionary<WealthType, float> WealthDict
    {
        get => _wealthDict;
        set {; }
    }
}
