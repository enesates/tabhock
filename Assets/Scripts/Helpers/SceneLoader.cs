using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType {
  MainMenuScene,
  LoadingScene,
  GameScene
}

public static class SceneLoader {
  public static void Load(SceneType scene) {
    SceneManager.LoadScene(scene.ToString());
  }

  public static void LoadWithLoading(SceneType targetScene) {
    PlayerPrefs.SetString("NextScene", targetScene.ToString());
    Load(SceneType.LoadingScene);
  }
}
