using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object data yang mendefinisikan objek customer di dalam game,
/// yang menyimpan data-data variabel dari customer.
/// </summary>
[CreateAssetMenu(fileName = "Customer", menuName = "AksaraBatik/Customer")]
public class Customer : ScriptableObject
{
    // Id atau kode identifier dari customer
    public string customerId;

    // Nama dari customer
    public string customerName;

    // Deskripsi dari customer
    public string customerDesc;

    // Dialog secara utuh dari customer
    public string customerDialog;

    // List dialog dari customer
    public List<string> customerDialogList;

    // foto potrait dari customer
    public Texture2D customerImage;

    // sprite dari customer
    public Sprite customerSprite;

    // Kain yang dinginkan oleh customer
    public Fabric customerFabric;

    // Batik yang diinginkan oleh customer
    public Batik customerBatik;

    // Tipe customer berdasarkan kekayaan
    public WealthType customerWealth;

    // Tipe customer berdasarkan perfeksionis
    public PerfectType customerPerfect;

    // Warna batik yang ia inginkan
    public BatikcolorType customerBatikColor;
}
