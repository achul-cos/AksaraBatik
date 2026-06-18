using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class yang memiliki properti dictionary yang mengonversi tipe perfect dengan nilai minimum perfect
/// </summary>
public static class Perfect
{
    private static readonly Dictionary<PerfectType, float> _perfectDict = new Dictionary<PerfectType, float>()
    {
        {PerfectType.D, 0.5f },
        {PerfectType.C, 0.6f },
        {PerfectType.B, 0.7f },
        {PerfectType.A, 0.8f },
        {PerfectType.S, 0.9f }
    };

    public static Dictionary<PerfectType, float> PerfectDict => _perfectDict;
}
