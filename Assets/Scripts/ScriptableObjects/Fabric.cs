using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Object data atau scriptable object yang mendefinisikan kain didalam game
/// </summary>
[CreateAssetMenu(fileName = "Fabric", menuName = "AksaraBatik/Fabric")]
public class Fabric : ScriptableObject
{
    // Nama kain
    public string fabricName;

    // Deskripsi Kain
    [TextArea(3, 12)] public string fabricDesc;

    // foto potrait dari kain
    [ShowAssetPreview(512, 512)] public Texture2D fabricImage;

    // objek kain didalam game
    [ShowAssetPreview(512, 512)] public Sprite fabricSprite;

    // List keyword dari kain
    public List<string> fabricKeyword;

    // Harga kain
    public long fabricPrice;
}
