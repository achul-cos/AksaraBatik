using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton atau manager yang mengatur logika cutomer didalam game.
/// </summary>
public class CustomerManager : Singleton<CustomerManager>
{
    // CustomerManager Variabel

    [Header("Customers Settings")]

    // Jumlah maksimal antrian customer
    [SerializeField] private int _maxQueueSize = 3;

    // Database customer

    // ====================================================== //

    // Getter Variable

    // ====================================================== //

    // Event

    // ====================================================== //

    // Singleton variable

    /// <summary>
    /// Tidak akan dihapus saat berpindah scene 
    /// </summary>
    protected override bool PersistBetweenScenes => true;

    /// <summary>
    /// Fungsi yang dijalankan pertama kali saat game dimulai
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        //
    }

    // ====================================================== //
}

// Scriptable object adalah objet data yang dapat kita atur pada inspector,
// Serta tersimpan sebagai suatu file .asset sebagai data tunggal tanpa terkait
// sebagai variabel suatu class selamanya.

/// <summary>
/// Scriptable Object dari CustomerDatabase yang menyimpan data-data customer 
/// </summary>
[CreateAssetMenu(fileName = "CustomersDatabase", menuName = "AksaraBatik/CustomesDatabase")]
public class CustomersDatabase : ScriptableObject
{
    /// <summary>
    /// List customer yang didaftarkan 
    /// </summary>
    public List<Customer> customers = new List<Customer>();

    /// <summary>
    /// Mengambil data customer berdasarkan ID nya
    /// </summary>
    /// <param name="id">Id Customer</param>
    /// <returns></returns>
    public Customer GetCustomerById(string id)
    {
        foreach (Customer customer in customers)
        {
            if(customer.customerId == id)
            {
                return customer;
            }
        }

        Debug.LogError($"CustomersDatabase [GetCustomerById] Error : Tidak ditemukan customer dengan Id {id}");
        return null;
    }

    /// <summary>
    /// Fungsi untuk 'Mengacha' Customer bedasarkan chance nya
    /// </summary>
    /// <param name="customerWealthMiskinChance">Kemungkinanan kedatangan customer bertipe kekayaan miskin</param>
    /// <param name="customerWealthBiasaChance">Kemungkinanan kedatangan customer bertipe kekayaan biasa</param>
    /// <param name="customerWealthKayaChance">Kemungkinanan kedatangan customer bertipe kekayaan kaya</param>
    /// <param name="customerWealthSultanChance">Kemungkinanan kedatangan customer bertipe kekayaan Sultan</param>
    /// <param name="customerPerfectDChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu D (50%)</param>
    /// <param name="customerPerfectCChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu C (60%)</param>
    /// <param name="customerPerfectBChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu B (70%)</param>
    /// <param name="customerPerfectAChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu A (80%)</param>
    /// <param name="customerPerfectSChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu S (90%)</param>
    public void GetCustomerRandom
    (
        float customerWealthMiskinChance = 1,
        float customerWealthBiasaChance = 1,
        float customerWealthKayaChance = 1,
        float customerWealthSultanChance = 1,
        float customerPerfectDChance = 1,
        float customerPerfectCChance = 1,
        float customerPerfectBChance = 1,
        float customerPerfectAChance = 1,
        float customerPerfectSChance = 1
    )
    {
        float customerWealthChanceTotal = (customerWealthMiskinChance + customerWealthBiasaChance + customerWealthKayaChance + customerWealthSultanChance);
        float customerPerfectChanceTotal = (customerPerfectDChance + customerPerfectCChance + customerPerfectBChance + customerPerfectAChance + customerPerfectSChance);
    }
}

/// <summary>
/// Object data yang mendefinisikan objek customer di dalam game,
/// yang menyimpan data-data variabel dari customer.
/// </summary>
[System.Serializable]
public class Customer
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

/// <summary>
/// Object data yang mendefinisikan kain didalam game
/// </summary>
[System.Serializable]
public class Fabric
{
    // Nama kain
    public string fabricName;

    // Deskripsi Kain
    public string fabricDesc;

    // foto potrait dari kain
    public Texture2D fabricImage;

    // objek kain didalam game
    public Sprite fabricSprite;

    // List keyword dari kain
    public List<string> fabricKeyword;

    // Harga kain
    public long fabricPrice;
}

[System.Serializable]
public class Batik
{
    // Nama Batik
    public string batikName;

    // Deskripsi batik
    public string batikDesc;

    // foto potrait dari batik
    public Texture2D batikImage;

    // list keyword dari batik
    public List<string> batikKeyword;

    // pola batik yang akan digambarkan
    public Texture2D batikPattern;

    // Harga batik
    public long batikPrice;
}

/// <summary>
/// Tipe-tipe customer bedasarkan kekayaan
/// </summary>
public enum WealthType
{
    MISKIN,
    BIASA,
    KAYA,
    SULTAN
}

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

/// <summary>
/// Tipe-tipe customer berdasarkan perfeksionisme
/// </summary>
public enum PerfectType
{
    D,
    C,
    B,
    A,
    S
}

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

public enum BatikcolorType
{
    MERAH,
    JINGGA,
    KUNING,
    HIJAU,
    BIRU,
    NILA,
    UNGU
}

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