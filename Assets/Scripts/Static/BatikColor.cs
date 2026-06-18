using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class yang mengoversi enum BatikColorType menjadi data color format RGB
/// </summary>
public static class BatikColor
{
    private static readonly Dictionary<BatikcolorType, Color32> _batikColorDict = new Dictionary<BatikcolorType, Color32>()
    {
        { BatikcolorType.MERAH,  new Color32(255, 0, 0, 255) },      // Merah solid
        { BatikcolorType.JINGGA, new Color32(255, 165, 0, 255) },    // Jingga / Orange
        { BatikcolorType.KUNING, new Color32(255, 255, 0, 255) },    // Kuning
        { BatikcolorType.HIJAU,  new Color32(0, 255, 0, 255) },      // Hijau
        { BatikcolorType.BIRU,   new Color32(0, 0, 255, 255) },      // Biru
        { BatikcolorType.NILA,   new Color32(75, 0, 130, 255) },     // Nila / Indigo
        { BatikcolorType.UNGU,   new Color32(148, 0, 211, 255) }     // Ungu / Violet
    };

    public static Dictionary<BatikcolorType, Color32> BatikColorDict => _batikColorDict;
}
