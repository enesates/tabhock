using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
  [SerializeField] private Button playButton;
  [SerializeField] private Button optionsButton;
  [SerializeField] private Button quitButton;
  [SerializeField] private Canvas optionsUI;

  private void Awake() {
    AudioListener.volume = PlayerPrefs.GetFloat("AudioListener", 1f);
    Time.timeScale = 1f;

    playButton.onClick.AddListener(() => {
      SceneLoader.LoadWithLoading(SceneType.GameScene);
    });

    optionsButton.onClick.AddListener(() => {
      optionsUI.gameObject.SetActive(true);
    });

    quitButton.onClick.AddListener(() => {
      Application.Quit();
    });
  }
}
