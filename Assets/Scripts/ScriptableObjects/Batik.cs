using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object data atau scriptable object yang mendefinisikan sebuah objek batik
/// </summary>
[CreateAssetMenu(fileName = "Batik", menuName = "AksaraBatik/Batik")]
public class Batik : ScriptableObject
{
    // Nama Batik
    public string batikName;

    // Deskripsi batik
    [TextArea(3, 12)] public string batikDesc;

    // foto potrait dari batik
    public Texture2D batikImage;

    // list keyword dari batik
    public List<string> batikKeyword;

    // pola batik yang akan digambarkan
    public Texture2D batikPattern;

    // Harga batik
    public long batikPrice;
}
