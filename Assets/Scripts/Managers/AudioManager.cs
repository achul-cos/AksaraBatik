using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Volume Settings")]
    [Range(0, 100)] [SerializeField] private int _musicVolume = 50;
    [Range(0, 100)] [SerializeField] private int _sfxVolume = 100;

    public int MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = Mathf.Clamp(value, 0, 100);
            AdjustVolume(_musicSource, _musicVolume);
        }
    }

    public int SfxVolume
    {
        get => _sfxVolume;
        set {
            _sfxVolume = Mathf.Clamp(value, 0, 100);
            AdjustVolume(_sfxSource, _sfxVolume);
        }
    }

    [Header("Audio Source")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("BGM List")]
    public List<AlbumBGM> BgmClips;

    [Header("SFX List")]
    public List<AlbumSFX> SfxClips;

    // Dictionary untuk nama SFX dengan SFX
    public Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();

    // Dictionary untuk nama BGM dengan BGM
    public Dictionary<string, AudioClip> bgmDictionary = new Dictionary<string, AudioClip>();

    protected override void Awake()
    {
        base.Awake();

        InitializeSfxDictionary();
        InitializeBgmDictionary();

        // Inisiasi volume pada audio source pertama kali
        AdjustVolume(_musicSource, _musicVolume);
        AdjustVolume(_sfxSource, _sfxVolume);
    }

    private void InitializeSfxDictionary()
    {
        foreach(var sfx in SfxClips)
        {
            if (!sfxDictionary.ContainsKey(sfx.Name))
            {
                sfxDictionary.Add(sfx.Name, sfx.Clip);
            }
            else
            {
                Debug.LogWarning($"AudioManager : Error terdapat sound effect yang namanya duplikat, yaitu {sfx.Name}");
            }
        }
    }

    private void InitializeBgmDictionary()
    {
        foreach(var bgm in BgmClips)
        {
            if (!bgmDictionary.ContainsKey(bgm.Name))
            {
                bgmDictionary.Add(bgm.Name, bgm.Clip);
            }
            else
            {
                Debug.LogWarning($"AudioManager : Error terdapat background music yang namanya duplikat, yaitu {bgm.Name}");
            }
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

    public void PlaySFX(string SfxName)
    {
        _sfxSource.PlayOneShot(GetSFXAudioClipByName(SfxName));
    }

    public void PlayBGM(string BgmName)
    {
        var clip = GetBGMAudioClipByName(BgmName);

        // Jika musik yang sedang dimainkan oleh
        // _musicSource, sama dengan music yang diminta.
        // Maka permintaan dibatalkan.
        if (_musicSource.clip == clip) return;

        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    private void AdjustVolume(AudioSource audio, int Vol)
    {
        audio.volume = Vol / 100f;
    }
}

[System.Serializable]
public class AlbumSFX
{
    public string Name;
    public AudioClip Clip; 
}

[System.Serializable]
public class AlbumBGM
{
    public string Name;
    public AudioClip Clip;
}
