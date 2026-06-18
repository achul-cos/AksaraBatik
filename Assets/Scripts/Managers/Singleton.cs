//=====================================================================
// Singleton.cs
//=====================================================================
//
// Pemrograman game ini menggunakan design pattern singleton.
// Singleton memungkinkan sebuah objek sebagai Kepala atau Pengatur
// Utama dari antar-antar game object didalam scene. 
// 
// Tanpa singleton, suatu game object dapat melakukan interaksi atau
// hubungan dengan banyak sekali game object di dalam suatu scene.
// Hal ini tidak efektif jika suatu game memiliki banyak game object,
// dan bersifat dinamis.
// 
// Dengan singleton, suatu game object hanya perlu melakukan hubungan
// dengan satu game object yang disebut dengan Manager. Dan hal itu
// dilakukan juga oleh banyak atau hampir semua game object lainya
// di dalam scene itu. Setelah semua game object terhubung dengan
// manager.
// 
// Jika game object ingin meminta (get) sebuah data dari game,
// object lainya. Sebelumnya, game object tujuan akan memberikan datanya
// kepada manager. Dan manager menyimpanya hingga data tersebut diminta
// oleh game object yang membutuhkanya.
// 
// Pada dasarnya singleton menjadikan suatu objek di dalam scene menjadi
// perantara antar objek lainya.
// 
// Pada beberapa kondisi atau manager, manager dapat tetap hidup antar
// scene. Maksudnya, setiap perpindahan scene, game object dari scene
// sebelumnya akan di hapus. Tetapi kebanyakan manager, mereka tidak
// di hapus walau berpindah scene.
// 
// Alasan mengapa manager tidak dihapus, karena manager ini membawa
// data 'penting' yang tidak boleh hilang atau mati saat game ini masih
// berjalan. Maka keberadaan mereka tidak boleh hilang saat game masih
// berjalan.
// 
// Untuk menjadi sebuah game object manager, dia harus memiliki atau
// mewarisi identitas singleton itu sendiri, antara lain:
// 1. Mewarisi MonoBehaviour
//     artinya dapat menjalankan unity lifecyle Awake(), Start(),
//     Update(), dan dapat ditambahkan pada game object unity
//     sebagai component (pada inspector)
// 2. Mewarisi variable Instance
//     Pada dasarnya ini property yang dimiliki oleh manager yang
//     dipanggil oleh game object yang ingin mengambil suatu data
//     dimanager. Misal: GameManager.Instance.Time
//     Artinya dia ingin mengambil data Time (waktu) pada GameManager
//     Ini merupakan standar Singleton, jadi tidak dapat bentuknya,
//     GameManager.Time, karena pasti error
// 3. *Bersifat DontDestroyOnLoad()
//     kebanyakan object singleton berifat DontDestroyOnLoad(),
//     maksudnya dia tidak akan dihapus saat terdapat perpindahan scene.
//     Tetapi pada beberapa object singleton dapat tidak mewarisi
//     identitas ini. Karena pada variable PersistBetweenScenes,
//     bernilai False. Yang berarti keberadaan nya dapat dihapuskan
//     jika berpindah scene.
// 4. Berjumlah tunggal pada setiap scene,
//     sebagai kepala atau pengatur, tentu pasti jumlahnya hanya satu
//     atau tunggal. Yah namanya juga SINGLEton... Maka dengan itu
//     object singleton yang sama atau duplikat akan otomatis dihapus
//     oleh sistem. Agar tidak terjadi error 'Manager mana yang harus
//     aku hubungi? Kok ada dua?'
// 
// Kode ini pada dasarnya sebuah class atau template yang akan diwariskan
// atau digunakan pada suatu class yang akan digunakan sebagai object
// singleton atau manager. Jadi ini kayak SOP yang perlu dipenuhi
// suatu game object menjadi object singleton.
//
// Schematic :
// class [Manager_Name] : Singleton<[Manager_Name]>
//
// Example :
// class GameManager : Singleton<GameManager>
//
// Kata kunci :
// Singleton object / Script Singleton / Script Singleton objet : GameManager.cs
// Game object singleton / game object yang memiliki component singleton object : _GameManager (di scene)
// Class Singleton Object : class GameManager : Singleton<GameManager>

// Menggunakan modul atau tools bawaan dari Unity engine 
// seperti MonoBehaviour dan lainya
using UnityEngine;

/// <summary>
/// Class yang harus diwarisi oleh class yang ingin menjadi singleton atau manager
/// akan mewarisi,
/// 
/// <para />
/// 
/// Variable:
/// <para />
/// T.Instance = T : Instance yang merujuk singleton object
/// 
/// <para />
/// 
/// Method:
/// <para />
/// virtual Awake() : Auto Assignment nilai Instance, dan pencegah singleton duplikat
/// <para />
/// virtual OnApplicationQuit() : Mencegah singleton dipanggil setelah game dimatikan
/// </summary>
/// <typeparam name="T">class yang ingin menjadi singleton atau manager</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _applicationIsQuitting = false;

    // Variabel yang mendefinisikan apakah object singleton ini tidak
    // dihancurkan saat berpindah scene. Jika bernilai True, maka tidak
    // dihancurkan. Jika false, maka object akan dihancurkan saat
    // berpindah scene. Variabel ini dapat di-'timpa' nilainya oleh
    // object singleton yang mewarisi class ini. Jadi bisa bernilai false
    // Jika game object nya mengaturnya jadi false, maka dia dapat hancur
    // saat berpindah scene.
    //
    // Contoh:
    // protected override bool PersistBetweenScenes => false;
    //
    /// <summary>
    /// True = object singleton ini tidak akan dihapus saat berpindah scene,
    /// False = object singleton ini akan dihapus saat berpindah scene.
    /// </summary>
    protected virtual bool PersistBetweenScenes => true;

    // Setiap object singleton memiliki variabel atau properti Instance.
    // Saat suatu game object didalam scene ingin mengakses suatu variabel
    // atau data yang dimiliki oleh manager, mereka dapat melakukanya
    // dengan cara;
    //
    // Schematic:
    // [Manager_Name].Instance.[Property_Manager]
    //
    // Contoh:
    // GameManager.Instance.Time
    // 
    // Walau dapat dianggap sebuah varibel pada object singleton,
    // tetapi ada proses otomatis didalamnya jika nilai dari Instance
    // masih null. Atau program yang sudah dimatikan. Ini berkenaan
    // dengan pembuatan game object singleton otomatis hingga validasi
    // pembuatan object singleton saat program game telah dimatikan.
    //
    /// <summary>
    /// Properti yang dapat diakses oleh semua game object di dalam scene
    /// untuk mengakses variabel yang dimiliki oleh manager; 
    /// [Manager_Name].Instance.[Property_Manager]
    /// </summary>
    public static T Instance
    {
        // Setiap variabel secara default memiliki getter atau setter.
        //
        // Getter adalah fungsi default yang dijalankan setiap variable
        // ini diakses atau diambil datanya. Contoh: GameManager.Instance
        // Kode fungsi getter berada didalam get()
        //
        // Setter adalah fungsi default yang dijalankan setiap variable
        // diubah atau dideklarasikan ulang. Contoh: GameManager.Instance = ...
        // Kode fungsi setter berada didalam set()
        //
        // Untuk variabel Instance, seharusnya tidak dapat di Setter.
        // Itu mengapa tidak ada set() pada Instance.

        get
        {
            // Validasi, variabel _applicationIsQuitting mendefinisikan apakah
            // program gamenya masih berjalan atau tidak. jika program gamenya
            // sudah tidak berjalan. Maka kode didalam if() dijalankan.
            if (_applicationIsQuitting)
            {
                // Memberikan console log warning bahwa object singleton ini masih
                // dipanggil oleh game object lainya, bahkan saat program gamenya
                // sudah dimatikan.
                // Serta, mengembalikan nilai null dan menyelesaikan proses getter.
                Debug.LogWarning($"[Singleton<{typeof(T)}>] Instance requested after application quit. Returning null.");
                return null;
            }

            // Locking, setiap game object yang mengakses Instance dari object
            // singleton atau manager otomatis menjalankan sebuah kode get()
            // didalam Instance. Tetapi didalam get() ada proses assign atau
            // definisi nilai Instance jika awalnya bernilai null 
            // (saat sistem baru dimulai). Agar tidak terjadi bentrok atau
            // pembuatan game object singleton yang ganda. Maka dibuatlah
            // 'proses otomatis' ini bersifat locking atau antrian.
            //
            // Jadi setiap game object yang mengakses Instance, diharapkan antri
            // dan menunggu sebelum game object lainya selesai mendapat nilai
            // dari Instance tersebut.
            lock (_lock)
            {
                if (_instance == null)
                {
                    // Saat game object mengakses nilai Instance, pada dasarnya Instance
                    // memberikan nilai dari variabel _instance. Tetapi diawal program
                    // game dijalankan, nilai dari variable _instance masih null. Maka
                    // diperlukan assignment atau deklarasi nilai dari _instance dengan
                    // cara pertama yaitu FindObjectOfType<T>() mencari setiap game object
                    // di dalam scene yang memiliki component berupa T.
                    // Dimana T adalah class atau script dari object singleton.
                    // Hasil pencarian akan diteruskan menjadi nilai dari _instance
                    _instance = FindObjectOfType<T>();

                    // Jika hasil pencarian FindObjectOfType<T>() gagal, maka _instance
                    // akan bernilai null.
                    // Jika _inctance bernilai null maka sistem akan membuat sendiri
                    // suatu game object yang memiliki component dari class object singleton
                    // itu sendiri yang akan menjadi game object singleton.
                    if (_instance == null)
                    {
                        // Sistem akan membuat sebuah game object baru didalam scene
                        // sebagai wadah cari class object singleton dan diberi nama
                        // sesuatu dari nama object singleton sendiri contoh:
                        // GameManager (Singleton)
                        //
                        // Hasil dari pembuatan game object tersebut akan diteruskan
                        // pada nilai dari variabel go
                        GameObject go = new GameObject($"{typeof(T).Name}");

                        // Setelahnya, sistem akan menambahkan component pada game object 
                        // yang telah dibuat sesuai dengan variabel go. Component yang
                        // ditambahkan, yaitu class dari object singleton itu sendiri.
                        //
                        // Hasil dari pembuatan component class object singleton itu,
                        // akan diteruskan pada variable _instance yang menyimpan
                        // referensi ke object singleton itu sendiri.
                        _instance = go.AddComponent<T>();

                        // Karena setiap class object singleton itu mewarisi class singleton
                        // itu sendiri. Maka dideklarasikan lah sebuah variabel singleton,
                        // yang mendefinisikan class singleton yang diwarisi oleh class
                        // object singleton.
                        //
                        // Maksudnya jika _instance itu merujuk pada:
                        // class GameManager : Singleton<GameManager>
                        //
                        // Maka variabel singleton merujuk pada:
                        // Singleton<GameManager>
                        // itu sendiri.
                        //
                        // Jika class yang dirujuk oleh _instance tidak mewarisi
                        // class Singleton<T> maka nilai dari variable singleton
                        // adalah NULL.
                        var singleton = _instance as Singleton<T>;

                        // Jika variabel dari singleton tidak NULL, maka class
                        // yang dirujuk oleh _instance itu benar-benar mewarisi class
                        // Singleton<T>
                        //
                        // Dan jika memang class tersebut benar-benar class Singleton<T>,
                        // maka class Singleton<T> yang diwarisi oleh class _instance,
                        // memiliki variable PersistBetweenScenes.
                        //
                        // Dan jika memang class Singleton<T> yang diwarisi oleh class _instance,
                        // nilai dari variable PersistBetweenScenes yaitu True. Maka game object
                        // pada variable go yaitu game object singleton, tidak memiliki sifat,
                        // DontDestroyOnLoad(), artinya tidak boleh dihapus saat perpindahan
                        // Scene.
                        if (singleton != null && singleton.PersistBetweenScenes)
                            DontDestroyOnLoad(go);

                        // Setelahnya Sistem akan membuat console log untuk memberikan informasi
                        // bahwa Game Object Singleton dari <T> telah dibuat.
                        Debug.Log($"[Singleton<{typeof(T)}>] Created new instance.");
                    }

                    // Saat suatu game object didalam scene mengakses nilai Instance dari
                    // object singleton. Maka akan mengembalikan nilai dari _instance itu
                    // sendiri.
                    //
                    // Jika nilai dari _instance itu sendiri masih null, maka proses
                    // pengembalian nilai _instance akan ditunda sebelum proses otomatis
                    // assignment dari _instance selesai dilakukan.
                    //
                    // Tetapi jika variabel _instance telah memiliki nilai, maka fungsi
                    // get() hanya mengembalikan nilai dari _instance yang telah ada saja.
                }
                return _instance;
            }
        }
    }

    // Ketika suatu game object dibuat lalu ditambahkan component berupa
    // script dari suatu object singleton seperti:
    // class GameManager : Singleton<GameManager>
    // Maka game object tersebut menjadi game object singleton,
    // dimana berisi suatu object singleton,
    // yang dapat diakses properti Instance oleh banyak game object 
    // lainya seperti : GameManager.Instance.
    //
    // Tetapi properti Instance mengambil value atau nilai dari
    // variabel _instance. Sedangkan Pada mulanya program game dijalankan
    // variabel _instance masih bernilai NULL.
    //
    // Maka dijalankanlah suatu fungsi Awake() yang dijalankan setiap
    // kali game object singleton dibuat, atau dijalankan setiap kali
    // program game baru dijalankan jika sedari awal sudah dibuat
    // game object singletonya.
    //
    // Fungsi Awake() memiliki dua fungsi: Pertama, untuk melakukan
    // assignment pada variable _instance dengan nilai yiatu object
    // singleton itu sendiri, misal: GameManager.
    //
    // Kedua, mencegah duplikasi dua game object yang memiliki satu
    // component script singleton object yang sama. Karena akan hanya
    // ada satu game object singleton yang dapat diassign pada variable
    // _instance. Dan ketika nilai dari _instance tidak sama dengan
    // game object singleton yang menjalankan fungsi awake() ini, maka
    // dapat diasumsikan, dia bukan game object suatu singleton object
    // yang pertama kali dibuat. Atau sederhananya ini ganda atau duplikatnya.
    // Misal, pada scene ada dua GameManager:
    //
    // _GameManager, Component : [Transform, GameManager.cs, ...]
    // GameManager (Singleton), Component : [Transform, SpriteRenderer, GameManager.cs, ...]
    //
    // Maka salah satunya, akan dihapus karena hanya ada boleh satu
    // game object pada suatu scene yang bisa menggunakan component GameManager.cs
    //
    // Tambahan, karena fungsi Awake() dideklarasikan sebagai fungsi
    // virtual. Class singleton object yang mewarisi class Singleton<T>
    // dapat 'menimpa' atau membuat fungsi Awake() versi singleton itu
    // sendiri.
    //
    // Tapi perlu diperhatikan bahwa, sesuai standar singleton. Setiap
    // class singleton object, pada fungsi Awake(), harus menjalankan
    // fungsi Awake dari class yang diwariskannya, yaitu class Singleton<T>
    // berikut schemanya.
    //
    // overide Awake()
    // {
    //      base.Awake()        // Ini wajib
    //      ...                 // Sisanya dibawah bisa menyesuaikan
    //      ...                 // kebutuhan dari script singleton itu sendiri
    // }
    protected virtual void Awake()
    {
        // Asumsikan kita membuat game object yang berisi component
        // script suatu singleton object sebelum program game dijalankan.
        //
        // Maka variabel _instance dari singleton object tersebut masih NULL
        //
        // Jika variabel _instance adalah NULL, maka assign nilai variabel
        // _instance dengan singleton object itu sendiri, misal GameManager.
        //
        // Dan jika variabel PersistBetweenScenes dari singleton object itu
        // bernilai True, maka terapkan sifat DontDestroyOnLoad() pada
        // game object yang berisi komponent dari singleton object itu.

        lock (_lock)    // Lock System, untuk mencegah multi thread
        {
            if (_instance == null)
            {
                // assign nilai variabel _instance dengan singleton object itu sendiri
                _instance = this as T;

                // jika variabel PersistBetweenScenes dari singleton object itu
                // bernilai True, maka terapkan sifat DontDestroyOnLoad() pada
                // game object yang berisi komponent dari singleton object itu.
                if (PersistBetweenScenes)
                    DontDestroyOnLoad(gameObject);
            }

            // Tetapi jika singleton object pada game object tersebut, nilai variabel
            // _instance nya tidak sama dengan singleton object itu sendiri. Maka
            // berikan console log warning bahwa ada game object singleton yang
            // duplikat. Dan hapuskan game object tersebut.
            else if (_instance != this)
            {
                // berikan console log warning bahwa ada game object singleton yang duplikat
                Debug.LogWarning($"[Singleton<{typeof(T)}>] Duplicate instance found on {gameObject.name}, destroying duplicate.");

                // Dan hapuskan game object tersebut
                Destroy(gameObject);
            }
        }

    }

    // fungsi OnApplicationQuit() yaitu bagian dari lifecycle unity
    // yang dijalankan saat program game dimatikan atau diberhentikan
    //
    // pada fungsi OnApplicationQuit() di class Singleton<T>, dia menjalankan,
    // assign pada variable _applicationIsQuitting menjadi true,
    // yang menandai bahwa aplikasi sudah keluar. Ini berhubungan pada
    // fungsi getter pada variable Instance.
    //
    // overide OnApplicationQuit()
    // {
    //      base.OnApplicationQuit()
    //      ...
    //      ...
    // }
    protected virtual void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }

    // Ketika game object singleton ini dibuat secara otomatis oleh sistem,
    // dan jika dia merupakan duplikat dari singleton object sebelumnya,
    // maka dia akan otomatis dihapuskan karena itu merupakan konsep
    // utama dari singleton.

    // Jika game object saat telah dihancurkan masih saja dipanggil
    // atau diakses oleh game object lainya, maka kembalikan nilai
    // Instance atau _instance yaitu null.
    protected virtual void OnDestroy()
    {
        lock (_lock)
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}