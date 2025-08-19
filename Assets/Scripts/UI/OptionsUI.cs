using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour {
  [SerializeField] private Canvas optionsUI;
  [SerializeField] private Button audioButton;
  [SerializeField] private Button durationButton;
  [SerializeField] private Button backButton;

  private TextMeshProUGUI audioLevel;
  private TextMeshProUGUI matchDuration;

  public void Start() {
    int duration = PlayerPrefs.GetInt("GameDuration", 3);
    matchDuration = durationButton.GetComponentInChildren<TextMeshProUGUI>();
    matchDuration.text = $"Match Duration:\n{duration} min.";

    audioLevel = audioButton.GetComponentInChildren<TextMeshProUGUI>();
    audioLevel.text = $"Audio: {AudioListener.volume}";
  }

  private void Awake() {
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
    });
  }
}
