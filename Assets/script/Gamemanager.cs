using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;

    public int playerScore = 0;
    public int winScore = 500;
    public Text scoreText;
    public GameObject winPanel;
    public Text winScoreText;
    public Button retryButton;
    public Button mainMenuButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Ẩn panel khi trò chơi bắt đầu
        winPanel.SetActive(false);

        // Thêm sự kiện cho các button
        retryButton.onClick.AddListener(Retry);
        mainMenuButton.onClick.AddListener(GoToMainMenu);

        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        playerScore += points;
        UpdateScoreText();

        if (playerScore >= winScore)
        {
            WinGame();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + playerScore;
    }

    void WinGame()
    {
        // Hiển thị panel chiến thắng và dừng game
        winPanel.SetActive(true);
        winScoreText.text = "Your Score: " + playerScore;

        // Dừng tất cả các nhân vật và kẻ địch
        Time.timeScale = 0f;
    }

    private void Retry()
    {
        // Chơi lại màn chơi hiện tại
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GoToMainMenu()
    {
        // Chuyển đến màn hình chính
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Đảm bảo rằng bạn đã có một scene tên là "MainMenu"
    }
}