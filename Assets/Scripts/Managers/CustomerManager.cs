using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

/// <summary>
/// Singleton atau manager yang mengatur logika cutomer didalam game.
/// </summary>
public class CustomerManager : Singleton<CustomerManager>
{
    // CustomerManager Variabel

    // [Header("Customers Settings")]

    // Jumlah maksimal antrian customer
    [SerializeField] private int _customerMaxQueueSize = 3;

    // Database customer
    [SerializeField] [Expandable] private CustomersDatabase _customerDatabase;

    // Queue atau Antrian Customer
    private Queue<Customer> _customerQueue = new Queue<Customer>();

    // Customer sekarang
    private Customer _customerCurrent;

    // Jumlah customer yang telah dilayani
    private int _customerServedToday = 0;

    // ====================================================== //

    // Customer Spawner Variable

    // Waktu jeda spawn antar customer
    private float _spawnInterval = 15.0f;

    // Variasai jeda spawn antar customer
    private float _variationSpawnInterval = 5.0f;

    // Jumlah customer yang telah dispawn
    private int _customersSpawned = 0;

    // Jumlah customer yang harus dispawn pada day ini
    private int _customersToSpawned = 0;

    // Jadwal atau waktu yang dijadwalkan dimana customer akan dispawn nantinya, ketika Time.time telah menempuhnya
    private float _nextSpawntime = 0f;

    // Status permulaan untuk melakukan spawning customer
    private bool _isSpawning = false;

    // Konfigurasi day config day ini
    private DayConfig _currentDayConfig;

    // ====================================================== //

    // Getter Variable

    // Jumlah maksimal antrian customer
    public int CustomerMaxQueue => _customerMaxQueueSize;

    // Antrian Customer
    public Queue<Customer> CustomerQueue => _customerQueue;

    // Database Customer
    public CustomersDatabase CustomerDatabase => _customerDatabase;

    // Customer Sekarang
    public Customer CustomerCurrent => _customerCurrent;

    // Jumlah customer yang telah dilayani hari ini
    public int CustomerServedToday => _customerServedToday;

    // Jumlah pelanggan yang sedang antri didalam antrian
    public int CustomerQueueCount => _customerQueue.Count;

    // Status atau keterangan apakah antrian sudah full atau belum
    public bool IsQueueFull
    {
        get
        {
            return _customerQueue.Count >= _customerMaxQueueSize;
        }
    }

    // Apakah sekarang lagi spawning?
    public bool IsSpawning => _isSpawning;

    // ====================================================== //

    // Event

    // Delegate atau kontrak fungsi yang subscribe dengan event CustomerArrived
    public delegate void OnCustomerArrived(Customer customer);

    // Publisher event CustomerArrived
    public event OnCustomerArrived CustomerArrived;

    // Delegate atau kontrak fungsi yang subscribe dengan event CustomerServed
    public delegate void OnCustomerServed(Customer customer);

    // Publisher event CustomerServed
    public event OnCustomerServed CustomerServed;

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

        // Inisiasikan Customer Database
        InitializeCustomerDatabase();

        // Reset customer queue, jumlah customer yang dilayani serta customer yang sekarang dilayani
        // ResetDayCustomers();
    }

    /// <summary>
    /// Fungsi yang dijalankan setiap frame game.
    /// Jangan letakkan algoritma terlalu berat didalam fungsi ini.
    /// </summary>
    private void Update()
    {
        if (_isSpawning == true && GameManager.Instance.IsPause == false && GameManager.Instance.IsGamePlay == true && GameManager.Instance.IsGameOver == false && Time.time >= _nextSpawntime) SpawnCustomer();
    }

    // ====================================================== //

    /// <summary>
    /// Inisiasikan database customer
    /// </summary>
    private void InitializeCustomerDatabase()
    {
        if (_customerDatabase == null)
        {
            Debug.LogWarning("CustomerManager : Belum assign database customer");
        }
    }

    /// <summary>
    /// Reset customer pada hari ini
    /// </summary>
    public void ResetDayCustomers()
    {
        // Menghapus antrian customer
        _customerQueue.Clear();

        // Hapus customer sekarang
        _customerCurrent = null;

        // Mereset jumlah customer yang sedang dilayani sekarang
        _customerServedToday = 0;

        // Mereset jumlah customer yang telah dispawn
        _customersSpawned = 0;

        // Mereset jumlah customer yang akan dispawn pada day ini
        _customersToSpawned = 0;

        // Mereset waktu spawn customer selanjurnya
        _nextSpawntime = 0f;

        // Mereset konfigurasi hari pada customer manager
        _currentDayConfig = null;

        // Minitoring Console Log
        Debug.Log("CustomerManager : Mereset customer pada hari ini");
    }

    /// <summary>
    /// Fungsi untuk memasukkan objek customer didalam queue
    /// </summary>
    /// <param name="customer">Objek customer yang dimasuki</param>
    /// <returns>Status keberhasilan memasuki customer didalam queue</returns>
    public bool AddCustomerToQueue(Customer customer)
    {
        // Validasi bahwa antrian customer sudah penuh atau belum
        if (_customerQueue.Count >= _customerMaxQueueSize)
        {
            Debug.LogWarning($"CustomerManager [AddCustomerToQueue] : Tidak dapat menambahkan customer dengan ID {customer.customerId} dan namanya {customer.customerName} pada queue. Karena jumlah antrian pada {_customerQueue.Count} jumlah lebih dari sama dengan dengan maksimum antrian {_customerMaxQueueSize}");
            return false;
        }

        // Memasukkan customer kedalam antrian
        _customerQueue.Enqueue(customer);

        // Trigger Publisher CustomerArrive sambil memberikan parameter customernya
        CustomerArrived?.Invoke(customer);

        // Monitoring console log
        Debug.Log($"CustomerManager : Menambahkan customer dengan ID  {customer.customerId} dan namanya {customer.customerName} pada queue.");

        // Return true bahwa customer bisa ditambahkan didalam queue
        return true;
    }

    /// <summary>
    /// Mengambil Customer paling awal didalam antrian untuk dilayani
    /// </summary>
    /// <returns>Customer yang paling depan antrianya</returns>
    public Customer GetNextCustomer()
    {
        // Validasi bahwa antrian customer kosong atau tidak
        // Jika kosong maka berikan log warning bahwa kosong
        if (_customerQueue.Count == 0)
        {
            // Monitoring console log, bahwa antrian customer itu kosong
            Debug.LogWarning($"CustomerManager [GetNextCustomer] : Tidak dapat memberikan customer pada customer queue, karena antrian customer kosong atau {_customerQueue.Count}.");

            return null;
        }

        // Keluarkan customer pada antrian yang paling depan, dan menjadi customer yang sekarang dilayani
        _customerCurrent = _customerQueue.Dequeue();

        // Berikan objek data customer _customercurrent sebagai customer yang dilayani
        return _customerCurrent;
    }

    /// <summary>
    /// Menyelesaikan sesi pesan memesan currentCustomer, dan mengosongkan slot dari _currentCustomer
    /// </summary>
    public void FinishCurrentCustomer()
    {
        // Validasi jika customer current bernilai null,
        // maka berikan log warning belum ada current customer yang dapat diselesaikan
        if(_customerCurrent == null)
        {
            Debug.LogWarning("CustomerManager [FinishCurrentCustomer] : Tidak dapat menyelesaikan seni pesan memesan dengan current customer karena memang belum ada");
        }

        else if (_customerCurrent != null)
        {
            // Menambahkan jumlah customer yang telah dilayani pada hari ini
            _customerServedToday++;

            // Trigger publsher customer arrived bahwa customer current telah selesai dilayani dan memberikan data customer yang telah dilayani
            CustomerServed?.Invoke(_customerCurrent);

            // Monitoring console log, memberitahukan bahwa customer current telah dilayani serta jumlah customer yang telah dilayani
            Debug.Log($"CustomerManager : Telah selesai melayani customer dengan nama id {_customerCurrent.customerId} dan nama yaitu {_customerCurrent.customerName}, dengan jumlah sementara pelanggan yang telah dilayani yaitu {_customerServedToday}");

            // Menghapus slot currentCustomer
            _customerCurrent = null;
        }
    }

    /// <summary>
    /// Mengubah current customer, bahkan secara paksa
    /// </summary>
    /// <param name="customer">Objek customer yang ingin menjadi currentCustomer</param>
    public void SetCurrentCustomer(Customer customer)
    {
        // Mengambil data lama currentCustomer
        Customer oldCurrentCustomer = _customerCurrent;

        // mengubah currentCustomer dengan data customer pada parameter
        _customerCurrent = customer;

        // Monitoring console log
        Debug.Log($"CustomerManager [SetCurrentCustomer] Log : Mengubah costumer yang sedang dilayani yang sebelumnya Customer dengan ID - {oldCurrentCustomer.customerId} dan namanya {oldCurrentCustomer.customerName}. Menjadi Customer dengan id - {_customerCurrent.customerId} dan dengan nama {_customerCurrent.customerName}");

        // Menghapus oldCurrentCustomer;
        oldCurrentCustomer = null;

        return;
    }

    /// <summary>
    /// Mengambil data customer pada antrian atau customerQueue, menggunakan index (dimulai dari 0).
    /// Biasanya untuk kebutuhan UI
    /// </summary>
    /// <param name="index"></param>
    /// <returns>Index costomer pada queue</returns>
    public Customer PeekCustomerAtIndex(int index)
    {
        // Validasi INDEX tidak boleh negatif dan lebih besar sama dengan jumlah customer yang antri di customerQueue
        if (index < 0 || index >= _customerQueue.Count)
        {
            Debug.LogWarning($"CustomerManager [PeekCustomerAtIndex] Error : Tidak dapat memberikan data customer pada customerQueue dengan index ke - {index} yang sama dengan dengan urutan customer pada antrian ke - {index + 1}. Karena nilainya tidak valid. Bisa jadi karena {index} bernilai negatif atau lebih kecil dari 0. Atau bisa jadi pada index customerQueue ke - {index} lebih besar dari jumlah customer didalam customerQueue yaitu {_customerQueue.Count} sedang index ke - {index} setara pada urutan {index + 1} yang lebih besar dari jumlah customer didalam customerQueue yaitu {_customerQueue.Count}.");
            return null;
        }

        // Buat local variabel list dari customerQueue yang diconver menjadi lis
        Customer[] customerArray = _customerQueue.ToArray();

        // Mengembalikan data customer pada index didalam array
        return customerArray[index];
    }

    // ====================================================== //

    // Customer Spawner Function

    /// <summary>
    /// Sebuah fungsi untuk memulai sesi spawning customer
    /// </summary>
    public void StartSpawning(bool IsResume = false)
    {
        // Melakukan reset variable customer agar tidak bentrok dengan variable dari day sebelumnya karena class ini bersifat singleton
        if (IsResume == false) ResetDayCustomers();

        // Mengambil konfigurasi hari ini dari gameManager
        _currentDayConfig = GameManager.Instance.GetCurrentDayConfig();

        // Log, memberitahu bahwa menggunakan konfigurasi apa untuk spawnning
        Debug.Log(_currentDayConfig);

        // Mengubah status bahwa spawning dimulai
        _isSpawning = true;

        // Memberikan waktu untuk customer selanjutnya akan datang
        if (IsResume == false) _nextSpawntime = Time.time + GetRandomSpawnInterveal() + 15f;
        else if (_nextSpawntime < Time.time && IsResume == true) _nextSpawntime = Time.time + GetRandomSpawnInterveal();

        // Mengatur jumlah customer yang harus dispawn
        _customersToSpawned = _currentDayConfig.dayCustomers;

        // Log, memberitahu bahwa spawning dimulai
        Debug.Log($"CustomerManager [StartSpawning] : Spawning dimulai, customer selanjutnya akan datang pada waktu {_nextSpawntime} atau {_nextSpawntime - Time.time} detik kemudian.");
    }

    /// <summary>
    /// Sebuah fungsi untuk memberhentikan sesi spawning customer
    /// </summary>
    public void StopSpawning()
    {
        // Mengubah status spawning menjadi false, menandakan bahwa spawning telah diberhentikan
        _isSpawning = false;

        Debug.Log("CustomerManager [StopSpawning] : Spawning selesai atau diberhentikan.");
    }

    /// <summary>
    /// Fungsi untuk melakukan spawning customer
    /// </summary>
    public void SpawnCustomer()
    {
        // Jika customer yang telah dispawn lebih banyak sama dengan jumlah customer yang harus dispawn berdasarkan day config
        // Maka hentikan spawning.
        if (_customersSpawned >= _customersToSpawned)
        {
            StopSpawning();
            return;
        }

        // Untuk mengenerat sebuah customer, maka mula-mula kita bisa mengenrate tipe wealth dan perfect customer
        // Menggunakan fungsi pada customer database, serta dengan parameter DayConfig yang kita ambil
        Customer customerRandom = _customerDatabase.GetCustomerRandomByWealthAndPerfectType(
            _customerDatabase.GetCustomerWealthAndPerfectTypeRandom(
                customerWealthMiskinChance: _currentDayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.MISKIN)?.wealthChance ?? 0f,
                customerWealthBiasaChance: _currentDayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.BIASA)?.wealthChance ?? 0f,
                customerWealthKayaChance: _currentDayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.KAYA)?.wealthChance ?? 0f,
                customerWealthSultanChance: _currentDayConfig.dayWealthCustomersChances.FirstOrDefault(x => x.wealthType == WealthType.SULTAN)?.wealthChance ?? 0f,
                customerPerfectDChance: _currentDayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.D)?.perfectChance ?? 0f,
                customerPerfectCChance: _currentDayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.C)?.perfectChance ?? 0f,
                customerPerfectBChance: _currentDayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.B)?.perfectChance ?? 0f,
                customerPerfectAChance: _currentDayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.A)?.perfectChance ?? 0f,
                customerPerfectSChance: _currentDayConfig.dayPerfectCustomerChances.FirstOrDefault(x => x.perfectType == PerfectType.S)?.perfectChance ?? 0f
            )
        );

        // Selanjutnya customerRandom yang didapatkan, kita set menjadi currentCustomer
        // Karena AddCustomerToQueue memberikan return bool, jadi kita dapat mengetahui
        // Apakah kita berhasil menambahkan customer atau tidak.
        bool added = AddCustomerToQueue(customerRandom);

        if (added)
        {
            _customersSpawned++;
            Debug.Log($"CustomerManager [SpawnCustomer] : Berhasil mengenarate dan menambahkan customer kedalam antrian, dengan informasi customer sebagai berikut {customerRandom.customerName}");
        }
        else Debug.LogError($"CustomerManager [SpawnCustomer] : Gagal menambahkan customer yang telah digenerate kedalam antrian dengan informasi customer {customerRandom.customerName}");

        // Trigger Publisher Bahwa Customer telah terspawn (datang)
        CustomerArrived?.Invoke(customerRandom);

        // Jadwalkan untuk spawn customer selanjutnya
        _nextSpawntime = Time.time + GetRandomSpawnInterveal();
    }

    /// <summary>
    /// Fungsi yang memberikan waktu atau jeda spawn antar customer
    /// yang random berdasarkan waktu dasar spawn customer serta variasi waktu spawn customer
    /// </summary>
    /// <returns>Waktu jeda spawn customer selanjutnya</returns>
    public float GetRandomSpawnInterveal()
    {
        return _spawnInterval + Random.Range(-_variationSpawnInterval, _variationSpawnInterval);
    }
}