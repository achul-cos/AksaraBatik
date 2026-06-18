using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object class yang menyimpan variabel nama sfx atau sound effect,
/// dan referensi file audionya
/// </summary>
[System.Serializable]
public class AlbumSFX
{
    public string Name;
    public AudioClip Clip;
}