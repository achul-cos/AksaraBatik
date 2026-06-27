using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Singleton atau manager yang menangani transisi perpindahan antar scene.<para />
/// Mendukung fade in/fade out dan loading screen
/// </summary>
public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    // SceneTransitionManager Variable

    [Header("Transition Settings")]

    // Canvas UI untuk loading screen atau latar fade
    [SerializeField] private CanvasGroup _fadeCanvasGroup;

    // Status transisi di dalam game, default = false
    private bool _isTransitioning = false;

    // ====================================================== //

    // Getter Variable

    public bool IsTransitioning => _isTransitioning;

    // ====================================================== //

    // Event

    // Kontrak atau template fungsi yang subscribe atau terkait dengan
    // event SceneLoadStart
    public delegate void OnSceneLoadStart();

    // Kontrak atau template fungsi yang subscribe atau terkait dengan
    // ebent SceneLoadEnds
    public delegate void OnSceneLoadEnd();

    // Publisher SceneLoadStart
    public event OnSceneLoadStart SceneLoadStart;

    // Publisher SceneLoadEnd
    public event OnSceneLoadEnd SceneLoadEnd;

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

        InitializeCanvasTransition();
    }

    // ====================================================== //

    /// <summary>
    /// Jika fadeCanvasGroup tidak diisi atau null,
    /// maka sistem akan secara otomatis membuat
    /// </summary>
    public void InitializeCanvasTransition()
    {
        if (_fadeCanvasGroup == null)
        {
            // Buat game object parent nya
            GameObject gameObjectTransitionCanvas = new GameObject("Transition Canvas");

            // Tambahkan component canvas pada game object yang dibuat
            Canvas transitionCanvas = gameObjectTransitionCanvas.AddComponent<Canvas>();
            transitionCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            transitionCanvas.sortingOrder = 100;

            // Atur ukuran canvas agar menyesuaikan dengan ukuran layar
            CanvasScaler scaler = gameObjectTransitionCanvas.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            // Tambahkan canvas group pada game object transition, lalu assign pada variabel
            // _fadeCanvasGroup
            CanvasGroup canvasGroup = gameObjectTransitionCanvas.AddComponent<CanvasGroup>();
            _fadeCanvasGroup = canvasGroup;
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            // Tambahkan game objet panel hitam sebagai child dari game object canvas
            GameObject gameObjectBlackPanel = new GameObject("Black Panel");
            gameObjectBlackPanel.transform.SetParent(gameObjectTransitionCanvas.transform, false);

            // Berikan warna pada panel hitam yaitu warna hitam
            Image panelImage = gameObjectBlackPanel.AddComponent<Image>();
            panelImage.color = new Color(0f, 0f, 0f, 1.0f);

            // RectTransform agar Panel otomatis memenuhi seluruh layar (Stretch All)
            RectTransform rectTransform = gameObjectBlackPanel.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0); // Jangkar pojok kiri bawah
            rectTransform.anchorMax = new Vector2(1, 1); // Jangkar pojok kanan atas
            rectTransform.pivot = new Vector2(0.5f, 0.5f); // Titik pusat di tengah

            // Reset offset menjadi 0 agar benar-benar menempel di semua sisi layar
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }

        DontDestroyOnLoad(_fadeCanvasGroup);
    }

    /// <summary>
    /// Fungsi iterasi untuk menimbulkan fade dari sebuah UI Object yang berfungsi transisi atau loading screen antar scene.<para />
    /// Efek fade muncul ketika fungsi ini melakukan iterasi (looping) mengurangi alpha atau opacity dari object loading screen berdasarkan
    /// durasi yang ditentukan.
    /// </summary>
    /// <param name="targetAlpha">Opisitas atau fade yang ingin dituju</param>
    /// <param name="duration">Mau berapa lama efek fadenya, default = 1s</param>
    /// <returns></returns>
    public IEnumerator FadeScreen(float targetAlpha, float duration = 1f)
    {
        // Jika objek transisi atau loading antar scene belum di assign
        if (_fadeCanvasGroup == null)
        {
            // Inisiasi pembuatan objek transisi atau loading screen antar scene secara default
            InitializeCanvasTransition();
        }

        // Waktu yang berjalan pada iterasi
        float time = 0f;

        // Opasitas atau alpha awal dari objeknya
        float startOpacity = _fadeCanvasGroup.alpha;

        // Iterasi atau looping mengubah opasitas objek secara bertahap berdasarkan durasi iterasi yang berjalan
        while (time < duration)
        {
            time += Time.deltaTime;
            _fadeCanvasGroup.alpha = Mathf.Lerp(startOpacity, targetAlpha, time / duration);
            yield return null;
        }

        // Makesure opasitas dari objeknya sesuai target
        _fadeCanvasGroup.alpha = targetAlpha;
    }

    /// <summary>
    /// Fungsi untuk melakukan transisi dari scene awal hingga scene tujuan
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="transitionDuration"></param>
    /// <param name="delayTransition"></param>
    /// <param name="delayToTransition"></param>
    /// <returns></returns>
    public IEnumerator TransitionToScene(string sceneName, float transitionDuration, float delayTransition, float delayToTransition)
    {
        // Jika objek transisi atau loading screen tidak di assign sebelumnya
        // Maka inisiasikan dengan objek transisi atau loading screen secara default
        if (_fadeCanvasGroup == null) InitializeCanvasTransition();

        // Jika statusnya masih transisi, _isTransitioning == true, maka hentikan fungsi
        if(_isTransitioning == true)
        {
            Debug.LogWarning("SceneTransitionManager [TransitionToScene] : Transisi tidak bisa dijalankan jika status transisi (_isTransitioning) masih true.");
            yield break;
        }

        // Perubahan status menjadi sedang transisi
        _isTransitioning = true;

        // Trigger publisher saat scene load start
        SceneLoadStart?.Invoke();

        // Delay sebelum transisi dijalankan
        yield return new WaitForSeconds(delayToTransition);

        // Pada object UI transitions scene atau loading screen mulai menghalangi player untuk menganu-anukan UI lainya
        _fadeCanvasGroup.blocksRaycasts = true;
        _fadeCanvasGroup.interactable = true;

        // Fade Out Objek Transisi atau loading screen
        yield return StartCoroutine(FadeScreen(1f, transitionDuration));

        // Waktu delay saat delayTransition
        yield return new WaitForSeconds(delayTransition);

        // Try catch untuk melakukan perpindahan scene menggunakan sceneManager
        try
        {
            // Load Scene menggunakan sceneManager
            SceneManager.LoadScene(sceneName);

            // Monitoring console log, untuk menunjukkan bahwa kita sudah berpindah scene
            Debug.Log($"SceneTransitionManager : Berpindah scene ke {sceneName}");
        }
        catch(Exception e)
        {
            // Monitoring log error, jika terjadi kegagalan perpindahan scene
            Debug.LogError($"SceneTransitionManager [TransitionToScene] Error : Tidak dapat berpindah scene karena, kemungkinan karena {sceneName} tidak terdaftar di build settings, atau emang gak ada. Ini pesan debugnya {e.Message}");
        }

        // Memberikan buffer time untuk memastikan bahwa scene load dengan baik
        yield return new WaitForSeconds(0.1f);

        // Fade in
        yield return StartCoroutine(FadeScreen(0f, transitionDuration));

        // Trigger publisher scene load end
        SceneLoadEnd?.Invoke();

        // Delay transisi akan selesai dijalankan
        yield return new WaitForSeconds(delayToTransition);

        // Perubahan status menjadi sudah selesai transisi
        _isTransitioning = false;
    }

    public void LoadScene(string sceneName, float transitionDuration, float delayTransition, float delayToTransition)
    {
        if (_isTransitioning == true)
        {
            Debug.LogWarning("SceneTransitionManager [LoadScene] : Transisi tidak bisa dijalankan jika status transisi (_isTransitioning) masih true.");
            return;
        }

        StartCoroutine(TransitionToScene(sceneName: sceneName, transitionDuration: transitionDuration, delayTransition: delayTransition, delayToTransition: delayToTransition));
    }

    public void ReloadCurrentScene(float transitionDuration = 1.0f, float delayTransition = 1.0f, float delayToTransition = 1.0f)
    {
        LoadScene(SceneManager.GetActiveScene().name, transitionDuration: transitionDuration, delayTransition: delayTransition, delayToTransition: delayToTransition);
    }
}
