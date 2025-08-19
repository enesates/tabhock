using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {
  private void Start() {
    // StartCoroutine(LoadGameAsync());
    string sceneName = PlayerPrefs.GetString("NextScene", SceneType.GameScene.ToString());
    SceneManager.LoadScene(sceneName);
  }

  // IEnumerator LoadGameAsync() {
  //   AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);
  //   asyncLoad.allowSceneActivation = false;

  //   while (!asyncLoad.isDone) {
  //     if (asyncLoad.progress >= 0.9f) {
  //       yield return new WaitForSeconds(1);
  //       asyncLoad.allowSceneActivation = true;
  //     }

  //     yield return null;
  //   }
  // }
}
