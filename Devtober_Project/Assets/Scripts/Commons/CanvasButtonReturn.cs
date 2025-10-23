using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Author : Auguste Paccapelo

public class CanvasButtonReturn : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Singleton ----- \\

    public static CanvasButtonReturn Instance {get; private set;}

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    [SerializeField] private Button _buttonToMenu;
    [SerializeField] private Button _buttonQuit;

    // ----- Others ----- \\

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Debug.Log(nameof(CanvasButtonReturn) + " Instance already exist, destorying last added.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start() { }

    void Update() { }

    // ----- My Functions ----- \\

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        _buttonToMenu.gameObject.SetActive(false);
        _buttonQuit.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
        _buttonToMenu.gameObject.SetActive(true);
        _buttonQuit.gameObject.SetActive(false);
    }

    // ----- Destructor ----- \\

    protected virtual void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}