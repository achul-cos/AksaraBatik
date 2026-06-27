using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton atau Manager yang mengatur waktu yang berjalan didalam game.<para />
/// Jam game dalam format 24 jam. <para />
/// </summary>
public class TimeManager : Singleton<TimeManager>
{
    // TimeManager variable

    [Header("Game Time Settings")]
    
    // Toko dibuka pada jam 9 pagi atau 09.00 (secara default),
    // Sebenarnnya bisa disesuaikan sesuai game design.
    [SerializeField] private int _startHour = 9;

    // Toko ditutup pada jam 5 sore atau 17.00 (secara default), dapat disesuaikan nantinya
    [SerializeField] private int _endHour = 17;

    // Terdapat perbedaan kecepatan waktu berjalan pada skala waktu didalam game
    // dengan skala waktu dunia nyata.
    // Bahwa 8 Jam Dunia Game, setara dengan 20 Menit Dunia Game
    // Maka dibutuhkan sebuah ratio perbandingan waktu;

    // Asumsikan X = Dunia Game, Y = Dunia Nyata
    //
    // 8 Jam X = 8 Jam Y                            (1)
    // (8 * 60) Menit X = (8 * 60) Menit Y          (2) 1 jam = 60 menit
    //
    // Kita menginginkan bahwa 8 * 60 menit dunia game setara dengan 20 menit dunia nyata,
    //
    // (8 * 60) Menit X = 20 menit Y                (3) Ganti (8 * 60) Menit Y, menjadi 20 menit Y
    // Y = (8 * 60) / 20 Menit X                    (4) bagi setiap ruas dengan 20
    //
    // Maka, rasio perbadingan waktunya yaitu, (8 * 60) / 20 Menit
    //
    // Tetapi rasio ini jika dijumlahkan 480 / 20 = 24
    // Maka waktu didalam game berjalan 24 kali lebih cepat daripada dunia nyata.
    //
    // variabel _timeRatio digunakan mengkonversi waktu yang berjalan secara real time menjadi menjadi waktu dunia game,
    //
    // Maka rasio perbandingan wakutnya dibalik menjadi 20 / (8 * 60) = 1 / 24
    // Maka waktu didunia berjalan 24 kali lebih lambar dari waktu didunia game.

    // _timeRation adalah perbandingan waktu yang berjalan didunia nyata (yang 24 kali lebih lambat),
    // daripada waktu didunia game
    [SerializeField] private float _timeRatio = 20f / (8 * 60);

    // _timeMinute menjelaskan bahwa sekarang sudah ke menit berapa berjalan waktunya didalam dunia game
    private float _timeMinute = 0;

    // _isTimePaused menjelaskan bahwa waktu didunia game apakah berhenti atau tidak berhenti
    private bool _isTimePaused = false;

    // _isDayEnded menjelaskan status apakah hari yang berjalan didalam, game sudah berakhir atau tidak
    // Berdasarkan jam buka dan jam tutup toko
    private bool _isDayEnded = true;

    // ====================================================== //

    // Event

    /// <summary>
    /// Setiap fungsi yang ingin melakukan subscribe pada event TimeChanged,
    /// diharuskan memiliki dua parameter untuk hour dan minute.
    /// serta dengan return type void
    /// </summary>
    /// <param name="hour">Jam yang berjalan pada game</param>
    /// <param name="minute">Menit yang berjalan pada game</param>
    public delegate void OnTimeChanged(int hour, int minute);

    /// <summary>
    /// Publisher pada setiap perubahan waktu yang terjadi 
    /// </summary>
    public event OnTimeChanged TimeChanged;

    /// <summary>
    /// Setiap fungsi yang ingin melakukan subscribe pada event DayEnd,
    /// diharuskan parameter kosong, dan return type void
    /// </summary>
    public delegate void OnDayEnd();

    /// <summary>
    /// Publisher pada setiap perubahan atau trigger day end. 
    /// </summary>
    public event OnDayEnd DayEnd;

    // ====================================================== //

    // Getter and Setter Variable

    // Meminta jumlah waktu yang berjalan didalam game,
    // dan dapat menambah jumlah waktu yang berjalan didalam game dan menjalankan perintah menjalankan waktu RunTime(),
    // penambahan waktu hanya dilakukan secara private di dalam class
    public float TimeMinute
    {
        get => _timeMinute;
        private set
        {
            _timeMinute += value; // Jika ingin skemanya waktunya ditambahkan, bukan diubah nilainya.
        }
    }

    // Mengembalikan nilai apakah IsShopOpen dengan memberikan nilai perbandingan,
    // apakah jam sekarang diatas sama dengan dengan waktu jam buka dan masih dibawah jam tutup.
    public bool IsShopOpen
    {
        get
        {
            return GetCurrentHour() >= _startHour && GetCurrentHour() < _endHour;
        }

    }

    // Mengembalikan nilai apakah toko buka (true) atau tutup (false)
    public bool IsDayEnded => _isDayEnded;

    public int StartHour => _startHour;
    public int EndHour => _endHour;

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

    }

    // ====================================================== //

    private void Update()
    {
        // Menjalankan waktu didalam game secara real time
        if (_isTimePaused == false && GameManager.Instance.IsGameOver == false && _isDayEnded == false) RunTime();
    }

    // ====================================================== //

    /// <summary>
    /// Menjalankan waktu didunia ga
    /// </summary>
    public void RunTime()
    {
        // Jalankan waktu jika waktu tidak dipause, _isTimePaused bernilai false,
        // dan game belum GameOver, GameManager.Instance.IsGameOver bernilai false,
        // Maka jalankan waktu dengan mekanisme, penambahan variabel _timeMinute melalui set TimeMinute
        if (_isTimePaused == false && GameManager.Instance.IsGameOver == false && _isDayEnded == false)
        {
            // Sebelum menjalankan waktu dengan menambah nilai _timeMinute,
            // validasikan bahwa tokonya sudah tutup atau belum.
            // jika sudah tutup maka jangan tambahkan waktu lagi kocak.

            // Untuk mengetahui apakah waktu sekarang sudah mencapi waktu jam tutup toko,
            // maka kita harus mengetahui sekarang jam berapa.
            // Dan jika jam sekarang lebih besar daripada jam tutup toko, MAKA JALANKAN PERINTAH TUTUP TOKO;

            if (GetCurrentHour() >= _endHour)
            {
                HandleDayEnd();

                return;
            }

            // Tambahkan nilai _timeMinute melalui TimeMinute
            // Tambahkan dengan bantuan tool Time.deltaTime yang menghasilkan satuan waktu dunia realtime
            // waktu dunia realtime haruslah dikali dengan rasio perbandingan waktu yaitu waktu dunia game 24 kali lebih cepat daripada waktu dunia nyata
            // Karena rasio perbandingan dideklaraikan 1 time (dunia nyata) / 24 time (dunia game),
            // maka Time.deltaTime haruslah dibagi dengan rasio perbandingan,
            // karena pembagian dengan pecahan atau nilai desimal atau nilai yang lebih kecil
            // menghasilkan nilai yang lebih besar; 1 * 1/2 = 2; 1 * 1/24 = 24;

            TimeMinute = Time.deltaTime * _timeRatio * 10f;

            // Trigger Publisher TimeChanged
            // Serta memberikan data jam dan waktu game sekarang
            TimeChanged?.Invoke(hour: GetCurrentHour(), minute: GetCurrentMinute());
        }

        else
        {
            Debug.LogWarning($"TimeManager [RunTime] : Waktu tidak bisa dijalankan karena salah satu dari dua variable ini bersifat false, yaitu _isTimePaused : {_isTimePaused.ToString()} dan GameManager.Instance.IsGameOver : {GameManager.Instance.IsGameOver.ToString()}");
        }
    }

    /// <summary>
    /// Untuk mengetahui sekarang jam berapa didunia game
    /// </summary>
    /// <returns>Waktu jam dunia game</returns>
    public int GetCurrentHour()
    {
        // Karena satuan waktu terkecil yang berjalan pada game ini yaitu menit,
        // Maka untuk mengetahui sekarang jam berapa yaitu,
        // membagi (pembagian pembulatan kebawah) jumlah menit pada _timeMinute dengan 60 menit untuk mengetahui berapa jam,

        // Jumlah menit yang berjalan didunia game yang dibulatkan ke bawah
        int currentMinute = Mathf.FloorToInt(_timeMinute);

        // Jumlah jam yang berjalan didunia game,
        // Menambahkan jam bukan toko lalu pembagian timeMinute / 60
        int currentHour = _startHour + currentMinute / 60;

        // Mengembalikan nilai current hour
        return currentHour;
    }

    /// <summary>
    /// Untuk mengetahui sekarang menit berapa pada jam sekian didunia game
    /// </summary>
    /// <returns>waktu menit dunia game</returns>
    public int GetCurrentMinute()
    {
        // Jumlah menit yang berjalan di game, dibulatkan ke bawah,
        // lalu dilakukan pembagian modulus 60,
        // pembagian modulus, merupakan pembagian menghasil sisa bagi dari operasi,
        // misal, menitnya 243, maka modulus 60 yaitu 3, dan sesuai logika
        // bahwa menit 243, itu haruslah 09.00 + 04.03 = 13.03
        int currentMinute = Mathf.FloorToInt(_timeMinute) % 60;

        // Mengembalikan nilai menit sekarang
        return currentMinute;
    }
    
    /// <summary>
    /// Menjalankan perintah bahwa hari dimulai
    /// </summary>
    public void HandleDayStart()
    {
        // Untuk memulai hari, maka status isDayEnd yang berarti hari telah berakhir menjadi false
        _isDayEnded = false;

        // reset waktu menit game yang berjalan menjadi 0
        _timeMinute = 0;

        // memastikan bahwa waktu tidak dipause
        _isTimePaused = false;

        // monitoring console log, memberitahukan bahwa hari sudah dimulai
        Debug.Log("TimeManager : Hari dimulai");

        // Menjalan waktu didalam game
        RunTime();
    }

    /// <summary>
    /// Menjalankan perintah bahwa hari sudah selesai
    /// </summary>
    public void HandleDayEnd()
    {
        // Berhentikan waktu game yang berjalan
        _isTimePaused = true;

        // Untuk mengakhiri hari didalam game, maka statu isDayEnd menjadi true
        _isDayEnded = true;

        // Trigger publisher DayEnd
        DayEnd?.Invoke();

        Debug.Log("TimeManager : Hari sudah berakhir");
    }

    /// <summary>
    /// Mereset waktu yang berjalan pada hari di game
    /// </summary>
    public void ResetDayTime()
    {
        // Mengubah nilai waktu didalam game _timeMinute menjadi 0,
        // Dan waktu di game akan menjadi 09.00
        _timeMinute = 0f;
    }

    /// <summary>
    /// Memberhentikan atau pause waktu didalam game
    /// </summary>
    public void PauseGameTime()
    {
        // Mengubah status pause waktu didalam game menjadi true
        _isTimePaused = true;

        // Mengubah time scale yang berjalan pada Time,
        // Terutama pada RunTime(), menjadi 0, atau tidak ada penambahan waktu
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Melanjutkan atau resume waktu didalam game
    /// </summary>
    public void ResumeGameTime()
    {
        // Mengubah status pause waktu didalam game menjadi false
        _isTimePaused = false;

        // Mengambalikan time scale yang berjalan menjadi normal yaitu 1
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Ketika dijalankan, jika game awalnya pause, maka akan diubah menjadi resume.<para />
    /// Tetapi ketika game resume, maka game menjadi pause.<para />
    /// Biasanya untuk tombol UI.
    /// </summary>
    public void TogglePause()
    {
        // Jika status game tidak pause atau resume, maka jalankan perintah pause pada gameManager,
        // Yang sekaligus menjalankan perintah PauseGameTime
        if (!_isTimePaused)
        {
            GameManager.Instance.Pause();
        }

        // Tetapi jika status game di pause, maka jalankan perintah resume pada gameManager,
        // yang sekaligus menjalankan perintah ResumeGameTime
        else if (_isTimePaused)
        {
            GameManager.Instance.Resume();
        }
    }

    /// <summary>
    /// Dapatkan waktu dala format string, dengan format waktunya, HH:mm
    /// </summary>
    /// <returns>waktu dalam bentuk string</returns>
    public string GetTimeString()
    {
        // Mengembalikan sebuah string waktu berformay HH:mm
        return $"{GetCurrentHour():D2}:{GetCurrentMinute():D2}";
    }

    /// <summary>
    /// Memberikan nilai progress hari yang sudah berjalan didalam game.
    /// </summary>
    /// <returns>progress dari 0-1 hari yang berjalan didalam game.</returns>
    public float GetDayProgress()
    {
        // Untuk mendapatkan progress hari yang berjalan didalam game
        // Maka kita melakukan pembagian pada jumlah jam kerja toko (waktu tutup - waktu buka),
        // dengan jumlah jam yang sudah berjalan (GeCurrentHour - waktu buka),
        // Misal jam game yaitu jam 10.20, maka progressnya (10 - 9) / (17 - 9) = 1 / 8 = sekitar 0,1

        // Jumlah jam kerja toko
        int workingHour = _endHour - _startHour;

        // Jumlah jam kerja yang telah dilalui oleh pemain
        int currentWorkingHour = GetCurrentHour() - _startHour;

        // Pembagian pada jumlah jam kerja toko dengan jumlah jam yang sudah berjalan
        float currentDayProgress = Mathf.Clamp01((float)currentWorkingHour / workingHour);

        return currentDayProgress;
    }
}
