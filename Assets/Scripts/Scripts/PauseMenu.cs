using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private PlayerHUD playerHUD;
    private PlayerCharacter player;
    public TMP_Text survivalTimeText;
    public TMP_Text killCountText;
    public PlayerHUD playerTimer;
    private bool isPaused = false;
    public static bool isGameActive = true;

    [SerializeField] private AudioClip backgroundMusic;
    private AudioSource audioSource;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        pauseMenu.SetActive(false);
        gameOverPanel.SetActive(false);
        ResumeGameAtStart();
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.volume = 0.2f;
            audioSource.loop = true; 
            audioSource.Play();
        }
    }

    private void Update()
    {
        PauseInput();
        CheckPlayerHealth();
    }

    private void PauseInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void CheckPlayerHealth()
    {
        if (player != null && player.GetHealth() <= 0)
        {
            GameOver();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGameActive = false;
        isPaused = true;
        UnlockCursor();
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGameActive = true;
        isPaused = false;
        LockCursor();
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }

    public void Reset()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void GameOver()
    {
        playerHUD.HideHUD();
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        isGameActive = false;
        UnlockCursor();
        if (audioSource != null)
        {
            audioSource.Stop();
        }


        string survivalTime = playerTimer.timeText.text;
        survivalTimeText.text = "You Survived: " + survivalTime;

        SceneController sceneController = FindObjectOfType<SceneController>();
        int killCount = sceneController.GetTotalEnemyCount();
        killCountText.text = "Enemies Defeated: " + killCount;
    }

    private void ResumeGameAtStart()
    {
        playerHUD.ShowHUD();
        Time.timeScale = 1f;
        isGameActive = true;
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}