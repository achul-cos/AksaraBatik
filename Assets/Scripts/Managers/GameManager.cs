using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        set {;}
        get
        {
            return _state.ToString();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        //
    }

    // Load Scene
    public void LoadGameState(GameState gs)
    {
        switch (gs)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene("00_MainMenu");
                break;

            case GameState.CutScene:
                SceneManager.LoadScene("01_CutScene");
                break;

            case GameState.Lobby:
                SceneManager.LoadScene("O2_Lobby");
                break;
        }
    }

    // Play A Game
    public void Play()
    {
        // Fungsi untuk menjalankan game

        // Load Scene CutScene
        LoadGameState(GameState.CutScene);
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
}
