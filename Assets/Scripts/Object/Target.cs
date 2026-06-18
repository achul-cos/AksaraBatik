using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Konfigurasi dari target pada suatu phase
/// </summary>
[System.Serializable]
public class Target
{
    [HideInInspector] public string targetName;
    public int targetValue;
    public TargetType targetType;
}
