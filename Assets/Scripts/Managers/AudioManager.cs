using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager atau singleton yang mengatur audio di dalam game.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    // AudioManager Varible

    [Header("Volume Settings")]

    // nilai volume master
    [Range(0, 1)] [SerializeField] private float _masterVolume = 1.0f;

    // nilai volume music atau bgm
    [Range(0, 1)] [SerializeField] private float _musicVolume = 0.3f;

    // nilai volume sfx atau sound effect
    [Range(0, 1)] [SerializeField] private float _sfxVolume = 0.5f;

    [Header("Audio Source")]

    // AudioSource untuk music atau BGM
    [SerializeField] private AudioSource _musicSource;

    // AudioSource untuk sfx
    [SerializeField] private AudioSource _sfxSource;

    // List BGM atau music
    [Header("BGM List")]
    public List<AlbumBGM> BgmClips;

    // List SFX atau sound effect
    [Header("SFX List")]
    public List<AlbumSFX> SfxClips;

    // Dictionary untuk nama SFX dengan SFX
    public Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();

    // Dictionary untuk nama BGM dengan BGM
    public Dictionary<string, AudioClip> bgmDictionary = new Dictionary<string, AudioClip>();

    // ====================================================== //

    // Getter Variable

    public float MasterVolume
    {
        get => _masterVolume;
        set
        {
            _masterVolume = Mathf.Clamp01(value);
            UpdateVolume();
        }
    }

    public float MusicVolume
    {
        get => _musicVolume * _masterVolume;
        set
        {
            _musicVolume = Mathf.Clamp01(value);
            UpdateVolume();
        }
    }

    public float SfxVolume
    {
        get => _sfxVolume * _masterVolume;
        set {
            _sfxVolume = Mathf.Clamp01(value);
            UpdateVolume();
        }
    }

    public AudioSource MusicSource => _musicSource;

    public AudioSource SFXSource => _sfxSource;

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

        // Menjalankan inisiasi pengisian dictionary SFX dan BGM
        InitializeSfxDictionary();
        InitializeBgmDictionary();

        // Menajalan inisiasi audioSource
        InitializeAudioSource();
    }

    // ====================================================== //

    /// <summary>
    /// Inisiasikan AudioSource SFX dan BGM jika tidak ada,
    /// atau belum dibuat sebelumnya.
    /// </summary>
    private void InitializeAudioSource()
    {
        // jika audioSource untuk music atau BGM, belum diinput,
        // atau bernilai null, maka buat component audioSource untuk music
        if (_musicSource == null)
        {
            // Mengambil objek game object yang menggunakan component script audio manager ini
            GameObject _AudioManager = gameObject;

            // Pada game object AudioManager, ditambahkan komponen AudioSource
            _musicSource = _AudioManager.AddComponent<AudioSource>();

            // Pada music atau bgm audio source, musik dimainkan secara loop
            _musicSource.loop = true;

            // audio source music atau bgm tidak langsung play music saat game baru dimulai,
            // setidaknya harus dijalankan dulu lewat fungsi
            _musicSource.playOnAwake = false;
        }

        // Jika audiosource untuk sfx, belum diinput,
        // atau bernilai null, maka buat compnent audioSource untuk sfx
        if (_sfxSource == null)
        {
            // Mengambil objek game object yang menggunakan component script audio manager ini
            GameObject _AudioManager = gameObject;

            // Pada game object AudioManager, ditambahkan komponen AudioSource
            _sfxSource = _AudioManager.AddComponent<AudioSource>();

            // Pada sfx audio source, musik tidak dimainkan secara loop, atau cuman play one shoot
            _sfxSource.loop = false;

            // audio source music atau bgm tidak langsung play music saat game baru dimulai,
            // setidaknya harus dijalankan dulu lewat fungsi
            _sfxSource.playOnAwake = false;
        }

        UpdateVolume();
    }

    /// <summary>
    /// Inisiasi dictionary untuk sfx, jadi kita bisa play musik dengan masukin judul sfx nya aja,
    /// menggunakan tipe data string
    /// </summary>
    private void InitializeSfxDictionary()
    {
        // Jika pada list SfxClips atau album sfx memiliki isi, maka lakukan iterasi
        if (SfxClips.Count >= 1)
        {
            // Iterasikan semua sfx didalam album sfx, lalu daftarkan didalam dictionary sfx
            foreach(var sfx in SfxClips)
            {
                // Jika sfx yang didaftarkan namanya kosong atau clip atau file audionya kosong, skip aja
                if (sfx.Name == "" || sfx.Clip == null) continue;

                if (!sfxDictionary.ContainsKey(sfx.Name))
                {
                    sfxDictionary.Add(sfx.Name, sfx.Clip);
                }
                else
                {
                    Debug.LogWarning($"AudioManager : Error terdapat sound effect yang namanya duplikat, yaitu {sfx.Name}");
                }
            }

            if (sfxDictionary.Count == 0) Debug.LogWarning("AudioManager [InitializeSfxDictionary] : Gagal mendaftarkan album SFX ke dictionary. Kemungkinanan masalahnya karena anggota yang didaftarkan tidak memiliki nama, atau clip atau file audionya kosong.");
        }

        // Jika kosong, berikan console log bahwa kosong
        else
        {
            Debug.LogWarning("AudioManager [InitializeSfxDictionary] : Album SFX Kosong");
        }
    }

    /// <summary>
    /// Inisiasi dictionary untuk bgm atau music, jadi kita bisa play musik dengan masukin judul bgm atau musiknya aja,
    /// menggunakan tipe data string
    /// </summary>
    private void InitializeBgmDictionary()
    {
        // Jika pada list BgmClips atau album bgm memiliki isi, maka lakukan iterasi
        if (BgmClips.Count >= 1)
        {
            // Iterasikan semua bgm didalam album bgm, lalu daftarkan didalam dictionary bgm
            foreach (var bgm in BgmClips)
            {
                // Jika bgm yang didaftarkan namanya kosong atau clip atau file audionya kosong, skip aja
                if (bgm.Name == "" || bgm.Clip == null) continue;

                if (!bgmDictionary.ContainsKey(bgm.Name))
                {
                    bgmDictionary.Add(bgm.Name, bgm.Clip);
                }
                else
                {
                    Debug.LogWarning($"AudioManager : Error terdapat background music yang namanya duplikat, yaitu {bgm.Name}");
                }
            }

            if (bgmDictionary.Count == 0) Debug.LogWarning("AudioManager [InitializeBgmDictionary] : Gagal mendaftarkan album BGM ke dictionary. Kemungkinanan masalahnya karena anggota yang didaftarkan tidak memiliki nama, atau clip atau file audionya kosong.");
        }

        // Jika kosong, maka beritahukan console log bahwa album BGM Kosong
        else
        {
            Debug.LogWarning("AudioManager [InitializeBgmDictionary] : Album BGM Kosong");
        }
    }

    private AudioClip GetSFXAudioClipByName(string SfxName)
    {
        if(sfxDictionary.TryGetValue(SfxName, out AudioClip value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"AudioManager : Can't find sound effect for that's {SfxName} name.");
            return null;
        }
    }

    private AudioClip GetBGMAudioClipByName(string BgmName)
    {
        if(bgmDictionary.TryGetValue(BgmName, out AudioClip value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"AudioManager : Can't find background music for that's {BgmName} name.");
            return null;
        }
    }

    /// <summary>
    /// Play SFX dengan nama sfx itu sendiri yang terdaftar pada dictionary
    /// </summary>
    /// <param name="SfxName">Nama SFX pada dictionaty</param>
    /// <param name="volume">Request volume</param>
    public void PlaySFXName(string SfxName, float volume = 0f)
    {
        if (volume != 0f && volume > 0f)
        {
            _sfxSource.PlayOneShot(GetSFXAudioClipByName(SfxName), Mathf.Clamp01(volume));
        }
        else
        {
            _sfxSource.PlayOneShot(GetSFXAudioClipByName(SfxName));
        }
    }

    /// <summary>
    /// Play BGM sesuai nama bgm yang didaftarkan pada dictionary
    /// </summary>
    /// <param name="BgmName"></param>
    public void PlayBGMName(string BgmName)
    {
        AudioClip clip = GetBGMAudioClipByName(BgmName);

        // Jika musik yang sedang dimainkan oleh
        // _musicSource, sama dengan music yang diminta.
        // Maka permintaan dibatalkan.
        if (_musicSource.clip == clip) return;

        // Menghentikan BGM yang jalan
        StopBGM();

        // Menganti musik yang ada di music audioSource
        _musicSource.clip = clip;

        // Play Audio Source Music
        _musicSource.Play();
    }

    /// <summary>
    /// Play sfx dengan referensi file audio
    /// </summary>
    /// <param name="audio">referensi file audio</param>
    /// <param name="volume">request volume</param>
    public void PlaySFX(AudioClip audio, float volume = 0)
    {
        // Jika volumenya tidak 0 dan diatas 0f,
        // Maka dapat play sfx sesuai volume yang direquest
        if (volume != 0f && volume > 0f)
        {
            _sfxSource.PlayOneShot(audio, Mathf.Clamp01(volume));
        }
        else
        {
            _sfxSource.PlayOneShot(audio);
        }
    }

    /// <summary>
    /// Menghentikan paksa SFX
    /// </summary>
    public void StopSFX()
    {
        _sfxSource.Stop();
    }

    public void PlayBGM(AudioClip audio, float volume = 0)
    {
        // Jika musik yang sedang dimainkan oleh
        // _musicSource, sama dengan music yang diminta.
        // Maka permintaan dibatalkan.
        if (_musicSource.clip == audio) return;

        // Menghentikan BGM yang jalan
        StopBGM();

        // Menganti musik yang ada di music audioSource
        _musicSource.clip = audio;

        // Play Audio Source Music
        _musicSource.Play();
    }

    // Menghentikan BGM
    public void StopBGM(bool isFadeOut = true)
    {
        if (isFadeOut)
        {
            StartCoroutine(FadeOutBGM());
        }
        else
        {
            _musicSource.Stop();
        }
    }

    /// <summary>
    /// Fungsi untuk mengupdate volume pada audio source sesuai data volume sfx 
    /// dan music yang disesuaikan dengan master volume
    /// </summary>
    public void UpdateVolume()
    {
        _musicSource.volume = MusicVolume;
        _sfxSource.volume = SfxVolume;
    }

    private IEnumerator FadeOutBGM(float duration = 2f)
    {
        float time = 0f;
        float volume = _musicSource.volume;

        while(time < duration)
        {
            time += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(volume, 0f, time / duration);
            yield return null;
        }

        _musicSource.Stop();
        _musicSource.volume = volume;
    }
}
