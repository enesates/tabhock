using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager> {
  [SerializeField] private GameObject scoreBoardText;
  [SerializeField] private GameObject goalText;
  [SerializeField] private GameObject countdownText;
  [SerializeField] private GameObject gameOverCanvas;
  [SerializeField] private GameObject gameOverWinnerText;
  [SerializeField] private GameObject gameOverScoreText;
  [SerializeField] private TextMeshProUGUI clockText;
  [SerializeField] private Image timerImage;
  [SerializeField] private GameObject settingsMenuCanvas;
  [SerializeField] private Button settingsButton;
  [SerializeField] private Button resumeButton;
  [SerializeField] private Button optionsButton;
  [SerializeField] private Button mainMenuButton;
  [SerializeField] private Button rematchButton;
  [SerializeField] private Button gameOverRematchButton;
  [SerializeField] private Button gameOverMainMenuButton;
  [SerializeField] private Canvas optionsUI;
  [SerializeField] private Button audioButton;
  [SerializeField] private Button durationButton;
  [SerializeField] private Button backButton;

  private TextMeshProUGUI scoreBoard;
  private TextMeshProUGUI countdown;
  private TextMeshProUGUI gameOverWinner;
  private TextMeshProUGUI gameOverScore;
  private TextMeshProUGUI audioLevel;
  private TextMeshProUGUI matchDuration;
  private float hideGoalTextTimer = 3f;

  public override void Init() {
    settingsButton.onClick.AddListener(() => {
      settingsMenuCanvas.SetActive(true);
      Time.timeScale = 0f;
    });

    resumeButton.onClick.AddListener(() => {
      settingsMenuCanvas.SetActive(false);
    });

    rematchButton.onClick.AddListener(() => {
      Time.timeScale = 1f;
      SceneLoader.LoadWithLoading(SceneType.GameScene);
    });

    gameOverRematchButton.onClick.AddListener(() => {
      Time.timeScale = 1f;
      SceneLoader.LoadWithLoading(SceneType.GameScene);
    });

    optionsButton.onClick.AddListener(() => {
      optionsUI.gameObject.SetActive(true);
      settingsMenuCanvas.gameObject.SetActive(false);
    });

    mainMenuButton.onClick.AddListener(() => {
      SceneLoader.LoadWithLoading(SceneType.MainMenuScene);
    });

    gameOverMainMenuButton.onClick.AddListener(() => {
      SceneLoader.LoadWithLoading(SceneType.MainMenuScene);
    });

    audioButton.onClick.AddListener(() => {
      AudioListener.volume = AudioListener.volume % 10 + 1;
      PlayerPrefs.SetFloat("AudioListener", AudioListener.volume);
      audioLevel.text = $"Audio: {AudioListener.volume}";
    });

    durationButton.onClick.AddListener(() => {
      int duration = PlayerPrefs.GetInt("GameDuration", 3);
      PlayerPrefs.SetInt("GameDuration", duration % 10 + 1);
      matchDuration.text = $"Match Duration:\n{PlayerPrefs.GetInt("GameDuration", 3)} min.";
    });

    backButton.onClick.AddListener(() => {
      optionsUI.gameObject.SetActive(false);
      settingsMenuCanvas.SetActive(true);
    });
  }

  private void Start() {
    scoreBoard = scoreBoardText.GetComponent<TextMeshProUGUI>();
    countdown = countdownText.GetComponent<TextMeshProUGUI>();
    gameOverWinner = gameOverWinnerText.GetComponent<TextMeshProUGUI>();
    gameOverScore = gameOverScoreText.GetComponent<TextMeshProUGUI>();

    int duration = PlayerPrefs.GetInt("GameDuration", 3);
    matchDuration = durationButton.GetComponentInChildren<TextMeshProUGUI>();
    matchDuration.text = $"Match Duration:\n{duration} min.";

    audioLevel = audioButton.GetComponentInChildren<TextMeshProUGUI>();
    audioLevel.text = $"Audio: {AudioListener.volume}";
  }

  private void Update() {
    timerImage.fillAmount = GameManager.Instance.GetClockTimeNormalized();
  }

  public void UpdateScore(int playerScore, int aiScore) {
    scoreBoard.text = $"PLAYER    {playerScore} - {aiScore}   TEAM-AI";
    goalText.SetActive(true);
    gameOverCanvas.SetActive(false);

    Invoke(nameof(HideGoalText), hideGoalTextTimer);
  }

  private void HideGoalText() {
    goalText.SetActive(false);
  }

  public void StartCountdown() {
    countdownText.SetActive(true);

    clockText.gameObject.SetActive(true);

    int gameDuration = GameManager.Instance.GetGameDuration();
    clockText.text = $"{gameDuration:00}:00";
  }

  public void StopCountdown() {
    countdownText.SetActive(false);
    clockText.gameObject.SetActive(false);
  }

  public void UpdateCountdown(float countdownTimer) {
    countdown.text = Mathf.Ceil(countdownTimer).ToString();
  }

  public void GameOver(int playerScore, int aiScore) {
    gameOverCanvas.SetActive(true);
    gameOverWinner.text = (playerScore > aiScore) ? "YOU WIN!!!" : (playerScore == aiScore) ? "TIED!!!" : "TEAM-AI WIN!!!"; 
    gameOverScore.text = $"PLAYER    {playerScore} - {aiScore}   TEAM-AI";
    scoreBoardText.SetActive(false);
  }
}
