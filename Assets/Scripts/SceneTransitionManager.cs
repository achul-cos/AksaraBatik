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

    // Durasi transisi fade in/fade out antar scene
    [SerializeField] private float _fadeDuration = 0.5f;

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

    public IEnumerator FadeScreen(float targetAlpha, float duration = 1f)
    {
        if (_fadeCanvasGroup == null)
        {
            yield return null;
        }
    }
}
