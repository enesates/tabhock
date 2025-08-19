
public class MusicManager : MonoSingleton<MusicManager> {
  
  public override void Init() {
    DontDestroyOnLoad(gameObject);
  }
}
