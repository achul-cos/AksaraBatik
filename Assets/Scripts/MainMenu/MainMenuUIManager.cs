using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
    private Vector3 _phase1OriginalScale;

    [Space]

    // Phase 2
    [SerializeField] private GameObject _phase2;
    [SerializeField] private GameObject _phase2Image;
    [SerializeField] private TextMeshProUGUI _phase2Title;
    [SerializeField] private GameObject _phase2Lock;
    [SerializeField] private TextMeshProUGUI _Phase2LockTitle;
    private Vector3 _phase2OriginalScale;

    [Space]

    // Phase 3
    [SerializeField] private GameObject _phase3;
    [SerializeField] private GameObject _phase3Image;
    [SerializeField] private TextMeshProUGUI _phase3Title;
    [SerializeField] private GameObject _phase3Lock;
    [SerializeField] private TextMeshProUGUI _Phase3LockTitle;
    private Vector3 _phase3OriginalScale;

    [Space]

    // Phase 4
    [SerializeField] private GameObject _phase4;
    [SerializeField] private GameObject _phase4Image;
    [SerializeField] private TextMeshProUGUI _phase4Title;
    [SerializeField] private GameObject _phase4Lock;
    [SerializeField] private TextMeshProUGUI _Phase4LockTitle;
    private Vector3 _phase4OriginalScale;

    // ---------------------------------------- //

    // Chapter Sub Panel UI Component

    [Header("Chapter Sub Panel UI Component")]

    // Chapter Sub Panel Close Button
    [SerializeField] private Button _chapterSubPanelCloseButton;

    // Chapter Sub Panel Chapter Image
    [SerializeField] private GameObject _chapterSubPanelChapterImage;

    // Chapter Sub Panel Chapter Title
    [SerializeField] private TextMeshProUGUI _chapterSubPanelChapterTitle;

    // Chapter Sub Panel Chapter Description
    [SerializeField] private TextMeshProUGUI _chapterSubPanelChapterDesc;

    // Chapter Sub Panel Chapter Target Money
    [SerializeField] private TextMeshProUGUI _chapterSubPanelChapterTargetMoney;

    // Chapter Sub Panel Chapter Target Customers
    [SerializeField] private TextMeshProUGUI _chapterSubPanelChapterTargetCustomers;

    // Chapter Sub Panel Chapter Play Button
    [SerializeField] private Button _chapterSubPabelChapterPlayButton;

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
                    if (subpanel == sp)
                    {
                        subpanel.SetActive(true);

                        AnimationManager.Instance.ClipOn(subpanel);

                        AudioManager.Instance.PlaySFXName("sweep");
                    }

                    else
                    {
                        AnimationManager.Instance.ClipOut(subpanel);

                        AudioManager.Instance.PlaySFXName("sweep");
                    }
                }
            }
            else
            {
                sp.SetActive(true);

                AnimationManager.Instance.ClipOn(sp);

                AudioManager.Instance.PlaySFXName("sweep");
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
                    AudioManager.Instance.PlaySFXName("sweep_2");

                    AnimationManager.Instance.ClipOut(subpanel);
                }
            }
            else
            {
                AudioManager.Instance.PlaySFXName("sweep_2");

                AnimationManager.Instance.ClipOut(sp);
            }
        }
        else
        {
            Debug.LogWarning($"MainMenuUIManager [LoadPanel] : Tidak dapat menutup sub panel {sp.name} karena tidak terdaftar pada sub panel-panel didalam scene MainMenu.");
        }
    }

    public void LoadChapterSubPanel(PhaseConfig phaseConfig)
    {
        // Chapter Image
        _chapterSubPanelChapterImage.GetComponent<RawImage>().texture = phaseConfig.phaseImage;

        // Chapter Title 
        _chapterSubPanelChapterTitle.text = phaseConfig.phaseName;

        // Chapter Desc
        _chapterSubPanelChapterDesc.text = phaseConfig.phaseDesc;

        // Chapter Money and Customer Target
        foreach (Target t in phaseConfig.phaseTarget)
        {
            if (t.targetType == TargetType.Income)

            {
                _chapterSubPanelChapterTargetMoney.text = t.targetValue.ToString();
            }

            else if (t.targetType == TargetType.Customers)
            {
                _chapterSubPanelChapterTargetCustomers.text = t.targetValue.ToString();
            }
        }
    }

    public bool IsChapterUnlocked(int phase)
    {
        if (phase <= GameManager.Instance.CurrentPhase && phase > 0)
        {
            return true;
        }
        else
        {
            return false;
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

    // Initialize

    private void Awake()
    {
        InitializedFirstPanel();
        InitializedOriginalScale();
    }

    private void Start()
    {
        InitializeAddListeners();

        InitializeAddTriggers();

        InitializedVolumeSlider();

        GameManager.Instance.InitializeCurrentPhaseAndDay();

        InitialiazedChapterPanel();
    }

    private void InitializeAddListeners()
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

        // Chapter Sub Panel Play Button
        _chapterSubPabelChapterPlayButton.onClick.AddListener(OnChapterSubPanelPlayButton);
    }

    private void InitializeAddTriggers()
    {
        AddTrigger(_phase1, EventTriggerType.PointerEnter, OnPhaseOnePointerEnter);
        AddTrigger(_phase1, EventTriggerType.PointerExit, OnPhaseOnePointerExit);
        AddTrigger(_phase1, EventTriggerType.PointerClick, OnPhaseOnePointerClick);

        AddTrigger(_phase2, EventTriggerType.PointerEnter, OnPhaseTwoPointerEnter);
        AddTrigger(_phase2, EventTriggerType.PointerExit, OnPhaseTwoPointerExit);
        AddTrigger(_phase2, EventTriggerType.PointerClick, OnPhaseTwoPointerClick);

        AddTrigger(_phase3, EventTriggerType.PointerEnter, OnPhaseThreePointerEnter);
        AddTrigger(_phase3, EventTriggerType.PointerExit, OnPhaseThreePointerExit);
        AddTrigger(_phase3, EventTriggerType.PointerClick, OnPhaseThreePointerClick);

        AddTrigger(_phase4, EventTriggerType.PointerEnter, OnPhaseFourPointerEnter);
        AddTrigger(_phase4, EventTriggerType.PointerExit, OnPhaseFourPointerExit);
        AddTrigger(_phase4, EventTriggerType.PointerClick, OnPhaseFourPointerClick);
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

    private void InitializedOriginalScale()
    {
        _phase1OriginalScale = _phase1 ? _phase1.GetComponent<RectTransform>().localScale : new Vector3(1f, 1f, 1f);
        _phase2OriginalScale = _phase2 ? _phase2.GetComponent<RectTransform>().localScale : new Vector3(1f, 1f, 1f);
        _phase3OriginalScale = _phase3 ? _phase3.GetComponent<RectTransform>().localScale : new Vector3(1f, 1f, 1f);
        _phase4OriginalScale = _phase4 ? _phase4.GetComponent<RectTransform>().localScale : new Vector3(1f, 1f, 1f);
    }

    private void InitialiazedChapterPanel()
    {
        // Chapter / Phase 1
        _phase1Image.GetComponent<RawImage>().texture = GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(1).phaseImage;
        _phase1Title.text = GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(1).phaseName;
        _phase1Lock.SetActive(!IsChapterUnlocked(1));
        _Phase1LockTitle.text = $"{GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(1).phaseName} Masih Terkunci. Selesaikann {GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(1).phaseName} untuk membuka chapter ini.";

        // Chapter / Phase 2
        _phase2Image.GetComponent<RawImage>().texture = GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(2).phaseImage;
        _phase2Title.text = GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(2).phaseName;
        _phase2Lock.SetActive(!IsChapterUnlocked(2));
        _Phase2LockTitle.text = $"{GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(2).phaseName} Masih Terkunci. Selesaikann {GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(2 - 1).phaseName} untuk membuka chapter ini.";

        // Chapter / Phase 3
        _phase3Image.GetComponent<RawImage>().texture = GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(3).phaseImage;
        _phase3Title.text = GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(3).phaseName;
        _phase3Lock.SetActive(!IsChapterUnlocked(3));
        _Phase3LockTitle.text = $"{GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(3).phaseName} Masih Terkunci. Selesaikann {GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(3 - 1).phaseName} untuk membuka chapter ini.";

        // Chapter / Phase 4
        _phase4Image.GetComponent<RawImage>().texture = GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(4).phaseImage;
        _phase4Title.text = GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(4).phaseName;
        _phase4Lock.SetActive(!IsChapterUnlocked(4));
        _Phase4LockTitle.text = $"{GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(4).phaseName} Masih Terkunci. Selesaikann {GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(4 - 1).phaseName} untuk membuka chapter ini.";
    }

    // =======================================================

    // Add Trigger

    private void AddTrigger(GameObject gameObject, EventTriggerType triggerType, UnityAction<BaseEventData> callback)
    {
        if (gameObject == null)
        {
            return;
        }

        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>() ?? gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = triggerType
        };

        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
    }

    // =======================================================

    // Button Trigger Function

    private void OnPlayButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        InitialiazedChapterPanel();

        LoadPanel(_chapterPanel);
    }

    private void OnSettingButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        LoadPanel(_settingPanel);
    }

    private void OnCreditButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        LoadPanel(_creditPanel);
    }

    private void OnQuitButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        LoadSubPanel(_quitSubPanel);
    }

    private void OnSettingBackButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        LoadPanel(_mainMenuPanel);
    }

    private void OnCreditBackButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        LoadPanel(_mainMenuPanel);
    }

    private void OnChapterBackButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        LoadPanel(_mainMenuPanel);
    }

    private void OnChapterSubPanelCloseButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        CloseSubPanel(_chapterSubPanel);
    }

    private void OnQuitSubPanelCancelButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        CloseSubPanel(_quitSubPanel);
    }

    private void OnQuitSubPanelConfirmButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        GameManager.Instance.Quit();
    }

    private void OnChapterSubPanelPlayButton()
    {
        AudioManager.Instance.PlaySFXName("click");

        GameManager.Instance.Play();
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

    // ======================================================

    // Add Trigger to UI Component

    private void OnPhaseOnePointerEnter(BaseEventData data)
    {
        if (IsChapterUnlocked(1) == true)
        {
            AudioManager.Instance.PlaySFXName("hover");

            AnimationManager.Instance.HoverUp(_phase1);
        }
    }

    private void OnPhaseOnePointerExit(BaseEventData data)
    {
        if (IsChapterUnlocked(1) == true)
        {
            AnimationManager.Instance.HoverDown(_phase1, _phase1OriginalScale);
        }
    }

    private void OnPhaseOnePointerClick(BaseEventData data)
    {
        if (IsChapterUnlocked(1) == true)
        {
            AudioManager.Instance.PlaySFXName("click_2");

            AnimationManager.Instance.PunchClick(_phase1);

            LoadChapterSubPanel(GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(1));

            LoadSubPanel(_chapterSubPanel);
        }
    }

    // -----------------------------------------------------------

    private void OnPhaseTwoPointerEnter(BaseEventData data)
    {
        if (IsChapterUnlocked(2) == true)
        {
            AudioManager.Instance.PlaySFXName("hover");

            AnimationManager.Instance.HoverUp(_phase2);
        }
    }

    private void OnPhaseTwoPointerExit(BaseEventData data)
    {
        if (IsChapterUnlocked(2) == true)
        {
            AnimationManager.Instance.HoverDown(_phase2, _phase2OriginalScale);
        }
    }

    private void OnPhaseTwoPointerClick(BaseEventData data)
    {
        if (IsChapterUnlocked(2) == true)
        {
            AudioManager.Instance.PlaySFXName("click_2");

            AnimationManager.Instance.PunchClick(_phase2);

            LoadChapterSubPanel(GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(2));

            LoadSubPanel(_chapterSubPanel);
        }

    }

    // ------------------------------------------------------------

    private void OnPhaseThreePointerEnter(BaseEventData data)
    {
        if (IsChapterUnlocked(3) == true)
        {
            AudioManager.Instance.PlaySFXName("hover");

            AnimationManager.Instance.HoverUp(_phase3);
        }
    }

    private void OnPhaseThreePointerExit(BaseEventData data)
    {
        if (IsChapterUnlocked(3) == true)
        {
            AnimationManager.Instance.HoverDown(_phase3, _phase3OriginalScale);
        }
    }

    private void OnPhaseThreePointerClick(BaseEventData data)
    {
        if (IsChapterUnlocked(3) == true)
        {
            AudioManager.Instance.PlaySFXName("click_2");

            AnimationManager.Instance.PunchClick(_phase3);

            LoadChapterSubPanel(GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(3));

            LoadSubPanel(_chapterSubPanel);
        }

    }

    // ------------------------------------------------------------

    private void OnPhaseFourPointerEnter(BaseEventData data)
    {
        if (IsChapterUnlocked(4) == true)
        {
            AudioManager.Instance.PlaySFXName("hover");

            AnimationManager.Instance.HoverUp(_phase4);
        }
    }

    private void OnPhaseFourPointerExit(BaseEventData data)
    {
        if (IsChapterUnlocked(4) == true)
        {
            AnimationManager.Instance.HoverDown(_phase4, _phase4OriginalScale);
        }

    }

    private void OnPhaseFourPointerClick(BaseEventData data)
    {
        if (IsChapterUnlocked(4) == true)
        {
            AudioManager.Instance.PlaySFXName("click_2");

            AnimationManager.Instance.PunchClick(_phase4);

            LoadChapterSubPanel(GameManager.Instance.PhasesConfigDatabase.GetPhaseConfigByPhase(4));

            LoadSubPanel(_chapterSubPanel);
        }
    }

    // ------------------------------------------------------------

}
