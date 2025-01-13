using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas _pauseMenuCanvas;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private TextMeshProUGUI _deathText;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private string _menuSceneName = "GameMenu";
    
    private bool _isPaused;
    private bool _isDead;
    private bool _isVictory;

    void Start()
    {
        _pauseMenuCanvas.gameObject.SetActive(false);
        if (_deathText != null)
            _deathText.gameObject.SetActive(false);
        if (_winText != null)
            _winText.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPause()
    {
        if (_isDead || _isVictory) return;
        
        _isPaused = !_isPaused;
        SetPauseState();
    }

    public void Resume()
    {
        if (_isDead || _isVictory) return;
        
        _isPaused = false;
        SetPauseState();
    }

    public void OnDeath()
    {
        _isDead = true;
        _isPaused = true;
        
        if (_deathText != null)
            _deathText.gameObject.SetActive(true);
        if (_resumeButton != null)
            _resumeButton.SetActive(false);
            
        SetPauseState();
    }

    public void OnVictory()
    {
        _isVictory = true;
        _isPaused = true;
        
        if (_winText != null)
            _winText.gameObject.SetActive(true);
        if (_resumeButton != null)
            _resumeButton.SetActive(false);
            
        SetPauseState();
    }

    void SetPauseState()
    {
        _pauseMenuCanvas.gameObject.SetActive(_isPaused);
        Cursor.visible = _isPaused;
        Cursor.lockState = _isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        _playerInput.SwitchCurrentActionMap(_isPaused ? "UI" : "Player");
        Time.timeScale = _isPaused ? 0 : 1;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_menuSceneName);
    }
}