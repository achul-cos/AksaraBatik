using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// SaveManager menangani semua operasi save and load data game menggunakan JSON.<para />
/// Menggunakan Application.persistentDataPath sebagai lokasi penyimpanan data game.
/// </summary>
public class SaveManager : Singleton<SaveManager>
{

    // Lokasi menyimpan datanya
    private string _savePath;

    // Nama file data gamenya
    private const string _saveFileName = "aksara_batik_save.json";

    // ====================================================== //    

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

        // Assign variabel _savePath saat game pertama kali dimainkan
        _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);

        // Monitoring, Console Log, Memberikan informasi lokasi save file game
        Debug.Log($"SaveManager : Lokasi data game berada di {_savePath}");
    }

    // ====================================================== //

    /// <summary>
    /// Mengecek apakah ada data save game sebelumnya pada _savePath
    /// </summary>
    /// <returns>Jika True maka terdaoat data save game sebelumnya</returns>
    public bool HasSave()
    {
        return File.Exists(_savePath);
    }

    /// <summary>
    /// Menyimpan data game pada lokasi _savePath
    /// </summary>
    /// <param name="currentPhase">Urutan Phase/Fase yang disimpan saat player bermain</param>
    /// <param name="currentDay">Urutan Day/hari yang disimpan saat player bermain</param>
    /// <param name="currentBalance">Jumlah uang yang dimiliki oleh player saat player menyimpan data game</param>
    public void SaveGame(int currentPhase, int currentDay, long currentBalance)
    {
        // Membuat objek data save baru dengan memasukkan variabelnya dengan parameter fungsi
        SaveData data = new SaveData
        {
            phase = currentPhase,
            day = currentDay,
            balance = currentBalance,
            timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        // Mengubah objek data save yang dibuat menjadi Json
        string Json = JsonUtility.ToJson(data, prettyPrint: true);

        // Try Catch Error Handler, Jika error saat menyimpan data,
        // Tidak langsung membuat error satu sistem. Setidak memberikan debug log error
        try
        {
            // Menyimpan data save game yang sudah menjadi JSON pada lokasi _savePath
            File.WriteAllText(_savePath, Json);

            // Monitoring, Console Log, Memberikan informasi data game yang disimpan
            Debug.Log($"SaveManager : Save Data Game at Phase {currentPhase}, on day {currentDay}, with balance {currentBalance}");
        }

        // Jika tidak bisa menyimpan data save game, system akan menjalankan kode catch error,
        // untuk mencegah sistem error sepenuhnya, dan memberikan informasi debugging error.
        catch (System.Exception e)
        {
            // Monitoring, Console Log, Memberitahukan bahwa error saat menyimpan data game, dan memberikan errornya.
            Debug.LogError($"SaveManager [SaveGame] : Gagal menyimpan data save game. Berikut pesan errornya : {e.Message}");
        }
    }

    /// <summary>
    /// Mengambil data save game yang telah disimpan sebelumnya pada _savePath,<para />
    /// dan mengembalikan data tersebut yang akan digunakan oleh GameManager (contohnya).
    /// </summary>
    /// <returns>Data game seperti phase, day dan balance player.</returns>
    public SaveData LoadGame()
    {
        // Mengecek apakah ada data game yang tersimpan didalam folder game.
        // Jika tidak ada data game yang tersimpan, maka kembalikan nilai null,
        // sebagai respon bahwa tidak ada data game yang telah tersimpan sebelumnya.
        if(!HasSave())
        {
            // Monitoring Console Log bahwa tidak ada data game yang disimpan sebelumnya
            Debug.LogWarning("SaveManager [LoadGame] : Tidak ada data game yang telah disimpan sebelumnya.");

            // Mengembalikan nilai null
            return null;
        }

        // Try Catch error handler, untuk mengambil data game yang telah disimpan sebelumnya
        try
        {
            // Membaca sebuah file pada _savePath lalu mengambil dan memasukkan nya pada variabel JSON
            string json = File.ReadAllText(_savePath);

            // Mengubah data json itu menjadi data bertipe SaveData yang akan digunakan oleh GameManager,
            // Menggunakan tool JsonUtility yaitu FromJson lalu menyertakan tipe data tujuan yaitu SaveData.
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // Monitoring Console Log, memberitahukan berhasil mengambil data save game
            Debug.Log($"SaveManager : Berhasil mengambil data save game yang tersimpan. Dengan informasi phase ke-{saveData.phase}, dan Day ke-{saveData.day}, dan jumlah balance Rp.{saveData.balance}, dengan disimpan pada waktu {saveData.timeStamp}");

            // Mengembalikan nilai saveData yang akan digunakan oleh GameManager
            return saveData;
        }

        // Jika gagal mengambil data save game yang tersimpan, maka berikan log debugging dan pemberitahuan error,
        // tanpa mengacaukan satu sistem.
        catch (System.Exception e)
        {
            // Monitoring console log, memberitahukan terjadi kegagalan, dan memberikan debuggingnya
            Debug.LogError($"SaveManager [LoadData] : Terjadi kegagalan untuk mengambil data save gamenya. Sebenarnya ada filenya di foldernya karena keberadaan nya {HasSave()}, tetapi saat ingin diambil terjadi error, kemungkinan ada hubungan sama sistem device atau faktor lainya yaitu : {e.Message}");

            // Mengembalikan nilai null
            return null;
        }

    }

    /// <summary>
    /// Menghapus file atau data save game yang disimpan sebelumnya.<para/>
    /// Biasanya untuk reset game atau new game.
    /// </summary>
    public void DeleteSave()
    {
        // Mengecek apakah ada file data save game yang telah disimpan
        // sebelumnya.
        if (HasSave())
        {
            // Menghapus file data save game yang disimpan
            File.Delete(_savePath);

            // Monitoring, Console Log, Memberi tahu bahwa data save gamenya
            // Telah dihapus sebelumnya.
            Debug.Log("SaveManager : Berhasil menghapus data save game yang disimpan.");
        }
    }

    /// <summary>
    /// Memberikan data game yang disimpan pada file save data game
    /// biasanya ini untuk UI atau main menu. <para/>
    /// Pada dasarnya ini sama saja seperti fungsi LoadGame()
    /// </summary>
    /// <returns>Mengembalikan data game bertipe SaveData: phase, day, balance dan terakhir kali main.</returns>
    public SaveData GetGameData()
    {
        return LoadGame();
    }
}