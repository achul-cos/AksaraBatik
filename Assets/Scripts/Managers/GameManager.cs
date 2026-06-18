using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager adalah Singleton yang mengkoordinasikan state global game,
/// melacak phase, day, saldo pemain, dan flow keseluruhan game.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    // Tidak akan dihapus saat berpindah scene
    protected override bool PersistBetweenScenes => true;

    // GameState
    private GameState _state = GameState.MainMenu;
    public GameState State
    {
        get => _state;
        private set
        {
            // Mengubah nilai dari GameState Global
            _state = value;
        }
    }

    // Scene Information
    public string SceneName
    {
        set {; }
        get
        {
            return _state.ToString();
        }
    }

    // ====================================================== //

    [Header("Game State")]

    // Nilai default untuk phase saat pertama kali player bermain. yaitu Phase ke-1
    [SerializeField] private int _currentPhase = 1;

    // Nillai default untuk day saat pertama kali player bermain. Yaitu Day ke-1
    [SerializeField] private int _currentDay = 1;

    // Nilai default untuk money atau uang yang dimiliki player, saat pertama kali player bermain,
    // yaitu 0 (Rp. 0) Satuan nya rupiah jika visualnya, tapi secara data yaitu long (Int 64 bit)
    [SerializeField] private long _currentBalance = 0;

    [Header("Phase Configuration")]

    // Konfigurasi dari setiap phase.
    // Karena game secara desain terdapat 4 phase, phase pertama, kedua, ketiga dan final,
    // maka otomatis dibuat empat PhaseConfig.
    [Expandable] [SerializeField] private PhasesConfigDatabase _phasesConfigDatabase;

    // Nilai yang mendefinisikan apakah game over (kalah) atau tidak
    // Secara default tertulis false saat player pertama kali bermain gamenya.
    private bool _isGameOver = false;

    // Nilai yang mendefinisikan apakah gamenya sedang dipause atau tidak
    // Secara default tertulis false saat player pertama kali bermain gamenya.
    // Karena gak mungkin baru main langsung dipause oleh sistem.
    private bool _isPaused = false;

    // Sebuah variabel yang dapat dimasuki oleh suatu fungsi,
    // variabel ini dapat mengatur bagaimana return type dan parameter dari fungsi tersebut.
    public delegate void OnGameStateChanged();

    // Sebuah variabel event yang dapat diakses oleh class lainya,
    // class lainya yang mengakses variabel ini dapat mengaitkannya pada suatu fungsi di class tersebut.
    // Dan ketika variabel event disini ditrigger, OnPhaseChanged?.Invoke()
    // fungsi yang dikaitkan dengan variabel event ini akan dijalankan.

    public event OnGameStateChanged OnPhaseChanged;         // Publisher Phase
    public event OnGameStateChanged OnDayChanged;           // Publisher Day
    public event OnGameStateChanged OnBalanceChanged;       // Publisher Money
    public event OnGameStateChanged OnGameOver;             // Publisher GameOver
    public event OnGameStateChanged OnPauseGame;            // Publisher Pause

    // ====================================================== //

    // Getter Properties

    public int CurrentPhase => _currentPhase;
    public int CurentDay => _currentDay;
    public long CurrentBalance => _currentBalance;
    public string PhaseName => _phasesConfigDatabase.phaseConfigs[_currentPhase - 1].phaseName.ToString();
    public string DayName => _phasesConfigDatabase.GetDayName(_currentDay);
    public bool IsGameOver => _isGameOver;
    public bool IsPause => _isPaused;

    // ====================================================== //

    protected override void Awake()
    {
        base.Awake();

        InitializePhasesConfig();
    }

    // ====================================================== /

    private void InitializePhasesConfig()
    {
        if (_phasesConfigDatabase == null)
        {
            _phasesConfigDatabase = ScriptableObject.CreateInstance<PhasesConfigDatabase>();

            _phasesConfigDatabase.phaseConfigs = PhasesConfigDefault.phaseConfigs;
        }
    }

    /// <summary>
    /// Fungsi global untuk berpindah antar scene di dalam game.
    /// </summary>
    /// <param name="gs">Game State didalam game pada enum GameState</param>
    /// <param name="isTransition">Ingin menggunakan transisi?, Default = true</param>
    /// <param name="transitionDuration">Lama transisi antar scene, default = 1s</param>
    /// <param name="delayTransition">Lama loading screen, default = 1s</param>
    /// <param name="delayToTransition">Delay sebelum transisi, default = 0s</param>
    public void LoadGameScene(GameState gs, bool isTransition = true, float transitionDuration = 1f, float delayTransition = 1f, float delayToTransition = 0f)
    {
        // Menjalankan perintah berdasarakan parameter dimasukkan
        switch (gs)
        {
            // Pindah atau masuk ke main menu
            case GameState.MainMenu:

                // Jika tidak ingin ada transisi
                if (!isTransition)
                {
                    SceneManager.LoadScene("00_MainMenu");
                    break;
                }

                SceneTransitionManager.Instance.LoadScene("00_MainMenu", transitionDuration: transitionDuration, delayTransition: delayTransition, delayToTransition: delayToTransition);
                break;

            case GameState.CutScene:
                // Jika tidak ingin ada transisi
                if (!isTransition)
                {
                    SceneManager.LoadScene("01_CutScene");
                    break;
                }

                SceneTransitionManager.Instance.LoadScene("01_CutScene", transitionDuration: transitionDuration, delayTransition: delayTransition, delayToTransition: delayToTransition);
                break;

            case GameState.Lobby:
                // Jika tidak ingin ada transisi
                if (!isTransition)
                {
                    SceneManager.LoadScene("O2_Lobby");
                    break;
                }

                SceneTransitionManager.Instance.LoadScene("O2_Lobby", transitionDuration: transitionDuration, delayTransition: delayTransition, delayToTransition: delayToTransition);
                break;
        }
    }

    // Play A Game
    public void Play()
    {
        // Load Scene CutScene
        // LoadGameState(GameState.CutScene);

        // Load Scene Lobby
        LoadGameScene(GameState.Lobby);

        // Jika player belum pernah bermain, maka inisiasikan permainan baru
        if (SaveManager.Instance.HasSave())
        {
            // Gunakan data permainan lama
            LoadGameData();
        }

        else
        {
            InitializeNewGame();
        }

        // Mulai Waktu Permainan
        TimeManager.Instance.HandleDayStart();

    }

    // Quit A Game
    public void Quit()
    {
        base.OnApplicationQuit();

        Debug.Log("Game is Quit");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
               Application.Quit();
#endif
    }

    /// <summary>
    /// Menginisiasi game yang baru dengan mengatur variabel GameManager
    /// </summary>
    private void InitializeNewGame()
    {
        // Pertama kali main, masih phase ke-1
        _currentPhase = 1;

        // Pertama kali main, masih day ke-1
        _currentDay = 1;

        // Pertama kali main, uang masih Rp.0
        _currentBalance = 0;

        // Pertama kali main, gamenya belum selesai
        _isGameOver = false;

        // Pertama kali main, gamenya tidak pause
        _isPaused = false;

        // Monitoring Purpose, Console Log dari data inisiasi game,
        Debug.Log($"GameManager : Game dimulai pada phase/fase ke-{_currentPhase}. Dan day/hari ke-{_currentDay}. Dan dengan jumlah uang sebanyak Rp.{_currentBalance}");
    }

    /// <summary>
    /// Ketika sebelumnya player sudah pernah bermain, dan ingin melanjutkan,
    /// Maka load data game sebelumnya, jadi gak perlu ngulang dari awal.
    /// </summary>
    private void LoadGameData()
    {
        SaveData saveData = SaveManager.Instance.LoadGame();

        if (saveData != null)
        {
            _currentPhase = saveData.phase;
            _currentDay = saveData.day;
            _currentBalance = saveData.balance;
            _isGameOver = false;
            _isPaused = false;

            Debug.Log($"GameManager : Load Previous Gameplay, with Phase - {saveData.phase}, on Day - {saveData.day}, and Balance Rp.{saveData.balance}");
        } else
        {
            Debug.LogWarning("GameManager [LoadGameData] : Can't load data Savedata, 'cause saveData is null. We initialize new game.");
            InitializeNewGame();
        }
    }

    /// <summary>
    /// Fungsi untuk menambahkan uang pemain
    /// </summary>
    /// <param name="balance">Jumlah uang yang ingin ditambahkan</param>
    public void AddBalance(long balance)
    {
        // Menambahkan _currentBalance dengan nilai balance
        _currentBalance += balance;

        // Mentrigger publisher Balance (OnBalanceChanged)
        OnBalanceChanged?.Invoke();

        // Update data save (local) pemain
        SaveGameData();
    }

    public void SubstractBalance(long balance)
    {
        // Mengurangi _currentBalance dengan nilai balance
        _currentBalance = Mathf.Max(0, (int)(_currentBalance - balance));

        // Mentrigger publisher Balance (OnBalanceChanged)
        OnBalanceChanged?.Invoke();

        // Update data save (local) pemain
        SaveGameData();
    }

    /// <summary>
    /// Fungsi untuk melanjutkan hari selanjutnya
    /// </summary>
    public void NextDay()
    {
        _currentDay++;

        // Jika hari esok merupakan phase baru,
        // maka lanjutkan ke phase selanjutnya.
        if (IsPhaseEnd())
        {
            NextPhase();
        }

        // Trigger publisher OnDayChanged
        OnDayChanged?.Invoke();

        // Update data save (local) pemain
        SaveGameData();

        // Play Gamenya
        Play();

        // Monitoring, Console Log, informasi hari apa ini dan sudah phase keberapa
        Debug.Log($"GameManager : Melanjutkan ke hari selanjutnya. Sekarang phase/fase ke-{-_currentPhase}, dan pada hari ke-{_currentDay}");
    }

    /// <summary>
    /// Melanjutkan ke phase keselanjutnya, tetapi jika phase selanjutnya sudah selesai, maka game over
    /// </summary>
    public void NextPhase()
    {
        // Jika phase sekarang (_currentPhase) masih dibawah jumlah phase yang ada digame (misal masih phase ke-1),
        // jumlah phase di dalam game ini berjumlah 4 (dapat disesuikan kembali)
        // Maka lanjutkan ke phase selanjutnya dan update datanya.
        if (_currentPhase < 4)
        {
            // Tambahkan phasenya
            _currentPhase++;

            // Jadikan nilai hari, pada hari pertama pada phase tersebut.
            // Maksudnya jika pada phase ini (misal phase ke-3),
            // awal dari hari dari phase ini yaitu x (awal hari dari phase ke-3 yaitu hari ke-7),
            // Maka jadikan nilai hari menjadi nilai x tersebut (hari ke-7)
            _currentDay = _phasesConfigDatabase.GetPhaseStartDay(_currentPhase);

            // Trigger Publisher OnPhaseChanged
            OnPhaseChanged?.Invoke();

            // Update data save (local) pemain
            SaveGameData();

            // Monitoring, Console log, memberikan informasi phase dan day keberapa
            Debug.Log($"GameManager : Melanjutkan ke Phase selanjutnya yaitu phase ke-{_currentPhase} dan pada hari ke-{_currentDay}");
        }

        // Tetapi jika phase sekarang (_currentPhase) ditambahkan 1,
        // menjadi tidak lebih kecil dari dibawah jumlah phase yang ada digame,
        // jumlah phase di dalam game ini berjumlah 4 (dapat disesuikan kembali);
        // Maka game sudah selesai, atau gameover.

        else if (_currentPhase + 1 >= 4)
        {
            // Mengubah nilai _isGameover menjadi true, yang berarti game sudah gameover
            _isGameOver = true;

            // Trigger publisher OnGameOver
            OnGameOver?.Invoke();

            // Monitoring, Console log, memberikan informasi bahwa game sudah game over
            Debug.Log($"GameManager : Game Ending, Game Over is {_isGameOver.ToString()}");
        }
    }

    /// <summary>
    /// Jika player gagal menyelesaikan target pada phase itu,
    /// player dapat mereset lagi harinya menuju hari pertama di phase tersebut.
    /// </summary>
    public void ResetToPhaseStart()
    {
        // Ubah hari ini, menjadi hari pertama pada scene ini
        _currentDay = _phasesConfigDatabase.GetPhaseStartDay(_currentPhase);

        // Trigger publisher OnDayChanged
        OnDayChanged?.Invoke();

        // reset status game over menjadi false (belum gagal)
        _isGameOver = false;

        // Trigger publisher OnGameOver
        OnGameOver?.Invoke();

        // Simpan perubahan data pemains
        SaveGameData();

        // Monitoring, Console Log
        Debug.Log($"GameManager : Reset Day To Start Day at Phase {_currentPhase}");
    }

    /// <summary>
    /// Jika player gagal mencapai target pada phase tersebut, dia akan game over
    /// </summary>
    public void GameOver()
    {
        // Game over is true
        _isGameOver = true;

        // trigger publisher OnGameOver
        OnGameOver?.Invoke();

        // Monitoring, Console Log
        Debug.Log($"GameManager : Target Phase {_currentPhase} tidak terpenuhi. Player Gagal.");
    }

    public void SaveGameData()
    {
        SaveManager.Instance.SaveGame(currentPhase: _currentPhase, currentDay: _currentDay, currentBalance: _currentBalance);
    }

    /// <summary>
    /// Mengetahui apakah hari yang dilanjutkan ini telah melewati phase sebelumnya
    /// </summary>
    /// <returns>Jika true, hari yang dilanjutkan ini, telah melewati phase sebelumnya</returns>
    private bool IsPhaseEnd()
    {
        int phaseEndDays = _phasesConfigDatabase.GetPhaseEndDay(_currentPhase);

        return _currentDay > phaseEndDays;
    }

    /// <summary>
    /// Fungsi untuk pause game
    /// </summary>
    public void Pause()
    {
        // Seharusnya ada logika pemberhentian waktu yang berjalan dan menghentikan segala logika fisika
        // saat game di pause, tapi belum dikembangkan saja.

        // Pemberhentian waktu didalam game
        TimeManager.Instance.PauseGameTime();

        // Game dipause
        _isPaused = true;

        // Trigger publisher onPauseGame
        OnPauseGame?.Invoke();

        // Monitoring console log, bahwa game sedang dipause
        Debug.Log("GameManager - TimeManager : Game di pause.");
    }

    /// <summary>
    /// Fungsi untuk resume atau melanjutkan game yang di pause
    /// </summary>
    public void Resume()
    {
        // Seharusnya ada melanjutkan logika pemberhentian waktu yang berjalan dan melanjutkan segala logika fisika
        // saat game sudah di resume kembali, tapi belum dikembangkan saja.

        // Melanjutkan waktu didalam game
        TimeManager.Instance.ResumeGameTime();

        // Game di resume
        _isPaused = false;

        // trigger publisher OnPauseGame
        OnPauseGame?.Invoke();

        // Monitoring console log, bahwa game sudah di resume.
        Debug.Log("GameManager - TimeManager : Game di resume.");
    }

    // Mengambil fungsi TogglePause pada timeManager untuk GameManager,
    // Untuk dipanggil oleh object lainnya melalui gameManager.
    public void TogglePause()
    {
        TimeManager.Instance.TogglePause();
    }
}