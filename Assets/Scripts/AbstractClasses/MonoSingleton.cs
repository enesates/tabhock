using UnityEngine;

// modified based on this: https://learn.unity.com/tutorial/monosingleton

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {
  private static T _instance;
  public static T Instance {
    get {
      if (_instance == null) {
        Debug.LogError(typeof(T).ToString() + " is NULL.");
      }

      return _instance;
    }
  }

  private void Awake() {
    if (_instance != null && _instance != this) {
      Destroy(gameObject);
      return;
    }

    _instance = this as T;
    Init();
  }

  public virtual void Init() {
  }
}