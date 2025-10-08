using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    [Header("UI References")]
    public Text winText;
    public Text loseText;
    public Text timerText;
    public GameObject pauseMenuUI;
    public CanvasGroup pauseMenuCanvasGroup;

    [Header("Game References")]
    public spawner spawner;
    public hero player;
    public AudioSource backgroundMusic;

    [Header("Buttons")]
    public GameObject continueButton;

    private float gameTime = 0f;
    private bool isCritical = false;
    private bool isPaused = false;
    private bool isGameOver = false;
    private float fadeSpeed = 5f;

    void Start()
    {
        Time.timeScale = 1f;

        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 1f;
            pauseMenuUI.SetActive(false);
        }

        if (winText != null) winText.gameObject.SetActive(false);
        if (loseText != null) loseText.gameObject.SetActive(false);
        if (continueButton != null) continueButton.SetActive(true);
    }

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }

        if (!isPaused)
        {
            gameTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(gameTime / 60);
            int seconds = Mathf.FloorToInt(gameTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";

            if (!isCritical && gameTime >= 120f)
            {
                isCritical = true;
                spawner.EnterCriticalMode();
            }

            if (gameTime >= 180f)
            {
                WinGame();
            }
        }

        if (pauseMenuCanvasGroup != null)
        {
            float targetAlpha = pauseMenuUI.activeSelf ? 1f : 0f;
            pauseMenuCanvasGroup.alpha = Mathf.MoveTowards(
                pauseMenuCanvasGroup.alpha,
                targetAlpha,
                Time.unscaledDeltaTime * fadeSpeed
            );
        }
    }

    // ====== GAME CONTROL ======

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);

        if (continueButton != null)
            continueButton.SetActive(true);

        if (winText != null) winText.gameObject.SetActive(false);
        if (loseText != null) loseText.gameObject.SetActive(false);

        Time.timeScale = 0f;

        if (backgroundMusic != null)
            backgroundMusic.Pause();

        pauseMenuCanvasGroup.alpha = 1f;
        pauseMenuCanvasGroup.interactable = true;
        pauseMenuCanvasGroup.blocksRaycasts = true;

    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;

        if (backgroundMusic != null)
            backgroundMusic.UnPause();
    }

    public void WinGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        if (spawner != null) spawner.StopSpawning();

        pauseMenuUI.SetActive(true);
        pauseMenuCanvasGroup.alpha = 1f;

        if (winText != null) winText.gameObject.SetActive(true);
        if (loseText != null) loseText.gameObject.SetActive(false);
        if (continueButton != null) continueButton.SetActive(false);

        Time.timeScale = 0f;
        if (backgroundMusic != null) backgroundMusic.Pause();
    }

    public void LoseGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        if (spawner != null) spawner.StopSpawning();

        pauseMenuUI.SetActive(true);
        pauseMenuCanvasGroup.alpha = 1f;

        if (winText != null) winText.gameObject.SetActive(false);
        if (loseText != null) loseText.gameObject.SetActive(true);
        if (continueButton != null) continueButton.SetActive(false);

        Time.timeScale = 0f;
        if (backgroundMusic != null) backgroundMusic.Pause();
    }


    // ====== BUTTONS ======
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
