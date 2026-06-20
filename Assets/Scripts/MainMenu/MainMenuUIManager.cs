using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MainMenuUIManager : MonoBehaviour
{
    // Main Menu Panel UI

    [Header("Panel Settings")]

    // Panek UI yang pertama kali akan muncul;
    [SerializeField] private GameObject _firstPanel;

    // Panel UI untuk bagian Main Menu
    [SerializeField] private GameObject _mainMenuPanel;

    // Panel UI untuk bagian Settings
    [SerializeField] private GameObject _settingPanel;

    // Panel UI untuk bagian Credit
    [SerializeField] private GameObject _creditPanel;

    // Panel UI untuk bagian Chapter
    [SerializeField] private GameObject _chapterPanel;


    // ======================================================================================================== //

    // Main Menu Sub Panel UI

    // Sub Panel UI pada bagian Chapter untuk menampilkan detail chapter
    [SerializeField] private GameObject _chapterSubPanel;

    // Sub panel confirmation quit
    [SerializeField] private GameObject _quitSubPanel;

    // ========================================================================================================= //

    // MainMenuPanel UI Component

    [Header("Main Menu Panel UI Component")]

    // tombol play
    [SerializeField] private Button _playButton;
    
    // tombol quit
    [SerializeField] private Button _quitButton;

    // tombol credit
    [SerializeField] private Button _creditButton;

    // tombol setting
    [SerializeField] private Button _settingButton;

    // ---------------------------------------- //

    // SettingPanel UI Component

    [Header("Setting Panel UI Component")]

    // Setting Back Button
    [SerializeField] private Button _settingBackButton;

    [Space]

    // Master Volume Slider
    [SerializeField] private Slider _masterVolumeSlider;

    // Master Volume Label
    [SerializeField] private TextMeshProUGUI _masterVolumeLabel;

    [Space]

    // Music Volume Slider
    [SerializeField] private Slider _musicVolumeSlider;

    // Music Volume Label
    [SerializeField] private TextMeshProUGUI _musicVolumeLabel;

    [Space]

    // SFX Volume Slider
    [SerializeField] private Slider _sfxVolumeSlider;

    // SFX Volume Label
    [SerializeField] private TextMeshProUGUI _sfxVolumeLabel;

    // ---------------------------------------- //

    // Credit Panel UI Component

    [Header("Credit Panel UI Component")]

    // credit Back Button
    [SerializeField] private Button _creditBackButton;

    // ---------------------------------------- //

    // Chapter Panel UI Component

    [Header("Chapter Panel UI Component")]

    // chapter Back Button
    [SerializeField] private Button _chapterBackButton;

    [Space]

    // Phase 1
    [SerializeField] private GameObject _phase1;
    [SerializeField] private GameObject _phase1Image;
    [SerializeField] private TextMeshProUGUI _phase1Title;
    [SerializeField] private GameObject _phase1Lock;
    [SerializeField] private TextMeshProUGUI _Phase1LockTitle;

    [Space]

    // Phase 2
    [SerializeField] private GameObject _phase2;
    [SerializeField] private GameObject _phase2Image;
    [SerializeField] private TextMeshProUGUI _phase2Title;
    [SerializeField] private GameObject _phase2Lock;
    [SerializeField] private TextMeshProUGUI _Phase2LockTitle;

    [Space]

    // Phase 3
    [SerializeField] private GameObject _phase3;
    [SerializeField] private GameObject _phase3Image;
    [SerializeField] private TextMeshProUGUI _phase3Title;
    [SerializeField] private GameObject _phase3Lock;
    [SerializeField] private TextMeshProUGUI _Phase3LockTitle;

    [Space]

    // Phase 4
    [SerializeField] private GameObject _phase4;
    [SerializeField] private GameObject _phase4Image;
    [SerializeField] private TextMeshProUGUI _phase4Title;
    [SerializeField] private GameObject _phase4Lock;
    [SerializeField] private TextMeshProUGUI _Phase4LockTitle;

    // ---------------------------------------- //

    // Chapter Sub Panel UI Component

    [Header("Chapter Sub Panel UI Component")]

    // Chapter Sub Panel Close Button
    [SerializeField] private Button _chapterSubPanelCloseButton;

    // ---------------------------------------- //

    [Header("Quit Sub Panel UI Component")]

    // Quit Sub Panel Confirm Button
    [SerializeField] private Button _quitSubPanelConfirmButton;

    // Quit Sub Panel Cancel Button
    [SerializeField] private Button _quitSubPanelCancelButton;

    // =======================================================

    // Load Panel and Sub Panel

    /// <summary>
    /// Load sebuah panel, dan menutup panel lamanya
    /// </summary>
    /// <param name="panel">Panel tujuan</param>
    public void LoadPanel(GameObject panel)
    {
        // Memasukkan semua gameObject menjadi satu array yang mengandung anggota-anggota panel yang terdaftar
        // didalam scene
        GameObject[] panels = new GameObject[]
        {
            _mainMenuPanel,
            _settingPanel,
            _creditPanel,
            _chapterPanel,
        };

        // Validasi bahwa panel yang diberikan haruslah berupa gameobject panel yang terdaftar pada scene
        if (panels.Contains(panel))
        {
            foreach (GameObject p in panels)
            {
                if (p == panel) p.SetActive(true);
                else p.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning($"MainMenuUIManager [LoadPanel] : Tidak dapat menampilka panel {panel.name} karena tidak terdaftar pada panel-panel didalam scene MainMenu.");
        }
    }

    /// <summary>
    /// Load Sub Panel pada scne
    /// </summary>
    /// <param name="sp">Game Object sup panel yang ingin ditampilkan</param>
    /// <param name="isCloseOtherSubPanel">Jika true, maka saat menampikan sup panel ini dia akan menutup sub panel lainya.</param>
    public void LoadSubPanel(GameObject sp, bool isCloseOtherSubPanel = true)
    {
        // Memasukkan semua gameObject menjadi satu array yang mengandung anggota-anggota sub panel yang terdaftar
        // didalam scene
        GameObject[] subpanels = new GameObject[]
        {
            _chapterSubPanel,
            _quitSubPanel
        };

        // Validasi bahwa panel yang diberikan haruslah berupa gameobject panel yang terdaftar pada scene
        if (subpanels.Contains(sp))
        {
            if (isCloseOtherSubPanel)
            {
                foreach (GameObject subpanel in subpanels)
                {
                    if (subpanel == sp) subpanel.SetActive(true);
                    else subpanel.SetActive(false);
                }
            }
            else
            {
                sp.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning($"MainMenuUIManager [LoadPanel] : Tidak dapat menampilka sub panel {sp.name} karena tidak terdaftar pada sub panel-panel didalam scene MainMenu.");
        }
    }

    /// <summary>
    /// Menutup sub panel
    /// </summary>
    /// <param name="sp">Game Object sup panel yang ingin ditutup</param>
    /// <param name="isCloseAllSubPanel">Jika true, akan menutup semua sup panel lainya</param>
    public void CloseSubPanel(GameObject sp, bool isCloseAllSubPanel = false)
    {
        // Memasukkan semua gameObject menjadi satu array yang mengandung anggota-anggota sub panel yang terdaftar
        // didalam scene
        GameObject[] subpanels = new GameObject[]
        {
            _chapterSubPanel,
            _quitSubPanel,
        };

        // Validasi bahwa panel yang diberikan haruslah berupa gameobject panel yang terdaftar pada scene
        if (subpanels.Contains(sp))
        {
            if (isCloseAllSubPanel)
            {
                foreach (GameObject subpanel in subpanels)
                {
                    subpanel.SetActive(false);
                }
            }
            else
            {
                sp.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning($"MainMenuUIManager [LoadPanel] : Tidak dapat menutup sub panel {sp.name} karena tidak terdaftar pada sub panel-panel didalam scene MainMenu.");
        }
    }

    // =======================================================

    // Variable

    // =======================================================

    // Event Subscribe

    /// <summary>
    /// Jika gameObject dari MainMenuUIManager aktif atau enable,
    /// maka subscribe fungsi didalam script ini pada pubslisher dari singleton atau manager didalam game.
    /// </summary>
    private void OnEnable()
    {
        //
    }

    /// <summary>
    /// Jika gameObject dari MainMenuUIManager aktif atau disablae,
    /// maka unsubscribe fungsi didalam script ini pada pubslisher dari singleton atau manager didalam game.
    /// </summary>
    private void OnDisable()
    {
        //
    }

    // =======================================================

    // Add Listener

    private void Start()
    {
        // Button trigger

        // PlayButton
        _playButton.onClick.AddListener(OnPlayButton);

        // SettingButton
        _settingButton.onClick.AddListener(OnSettingButton);

        // Credit Button
        _creditButton.onClick.AddListener(OnCreditButton);

        // Quit Button
        _quitButton.onClick.AddListener(OnQuitButton);

        // Setting Back Button
        _settingBackButton.onClick.AddListener(OnSettingBackButton);

        // Credit Back Button
        _creditBackButton.onClick.AddListener(OnCreditBackButton);

        // Chapter Back Button
        _chapterBackButton.onClick.AddListener(OnChapterBackButton);

        // Chapter Sub Panel Close Button
        _chapterSubPanelCloseButton.onClick.AddListener(OnChapterSubPanelCloseButton);

        // Quit Sub Panel Cancel Button
        _quitSubPanelCancelButton.onClick.AddListener(OnQuitSubPanelCancelButton);

        // Quit Sub Panel Confirm Button
        _quitSubPanelConfirmButton.onClick.AddListener(OnQuitSubPanelConfirmButton);

        // Master Volume Slider
        _masterVolumeSlider.onValueChanged.AddListener(OnValueChangedMasterVolume);

        // Music Volume Slider
        _musicVolumeSlider.onValueChanged.AddListener(OnValueChangedMusicVolume);

        // SFX Volume Slider
        _sfxVolumeSlider.onValueChanged.AddListener(OnValueChangedSFXVolume);

        InitializedVolumeSlider();
    }

    // Initialize

    private void Awake()
    {
        InitializedFirstPanel();
    }

    private void InitializedFirstPanel()
    {
        LoadPanel(_firstPanel);
    }

    private void InitializedVolumeSlider()
    {
        if(_masterVolumeSlider != null)
        {
            _masterVolumeSlider.value = AudioManager.Instance.MasterVolume;
        }

        if(_masterVolumeLabel != null)
        {
            _masterVolumeLabel.text = $"{Mathf.FloorToInt(AudioManager.Instance.MasterVolume * 100f)}%";
        }

        if(_musicVolumeSlider != null)
        {
            _musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
        }

        if(_musicVolumeLabel != null)
        {
            _musicVolumeLabel.text = $"{AudioManager.Instance.MusicVolume * 100f}%";
        }

        if(_sfxVolumeSlider != null)
        {
            _sfxVolumeSlider.value = AudioManager.Instance.SfxVolume;
        }

        if(_sfxVolumeLabel != null)
        {
            _sfxVolumeLabel.text = $"{AudioManager.Instance.SfxVolume * 100f}%";
        }
    }

    // =======================================================

    // Button Trigger Function

    private void OnPlayButton()
    {
        LoadPanel(_chapterPanel);
    }

    private void OnSettingButton()
    {
        LoadPanel(_settingPanel);
    }

    private void OnCreditButton()
    {
        LoadPanel(_creditPanel);
    }

    private void OnQuitButton()
    {
        LoadSubPanel(_quitSubPanel);
    }

    private void OnSettingBackButton()
    {
        LoadPanel(_mainMenuPanel);
    }

    private void OnCreditBackButton()
    {
        LoadPanel(_mainMenuPanel);
    }

    private void OnChapterBackButton()
    {
        LoadPanel(_mainMenuPanel);
    }

    private void OnChapterSubPanelCloseButton()
    {
        CloseSubPanel(_chapterSubPanel);
    }

    private void OnQuitSubPanelCancelButton()
    {
        CloseSubPanel(_quitSubPanel);
    }

    private void OnQuitSubPanelConfirmButton()
    {
        GameManager.Instance.Quit();
    }

    // =======================================================

    // Slider Trigger Function

    private void OnValueChangedMasterVolume(float currentValue)
    {
        AudioManager.Instance.MasterVolume = currentValue;

        _masterVolumeLabel.text = $"{Mathf.FloorToInt(AudioManager.Instance.MasterVolume * 100f)}%";
    }

    private void OnValueChangedMusicVolume(float currentValue)
    {
        AudioManager.Instance.MusicVolume = currentValue;

        _musicVolumeLabel.text = $"{Mathf.FloorToInt(AudioManager.Instance.MusicVolume * 100f)}%";
    }

    private void OnValueChangedSFXVolume(float currentValue)
    {
        AudioManager.Instance.SfxVolume = currentValue;

        _sfxVolumeLabel.text = $"{Mathf.FloorToInt(AudioManager.Instance.SfxVolume * 100f)}%";
    }


}
