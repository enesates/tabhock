using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
	[SerializeField] private PlayerController player;
	[SerializeField] private AIController ai;
	[SerializeField] private BallController ball;
	[SerializeField] private GoalController northGoal;
	[SerializeField] private GoalController southGoal;
	[SerializeField] private ParticleSystem goalParticleSystem;
	[SerializeField] private int gameplayTimerMax = 3;

	private enum State {
		Waiting,
		Countdown,
		Playing,
		GameOver,
	}
	private State state;
	private float waitingToStartTimer = 1f;
	private float countdownToStartTimer = 3f;
	private float gameplayTime = 0f;

	public override void Init() {
		AudioListener.volume = PlayerPrefs.GetFloat("AudioListener", 1f);
		gameplayTimerMax = PlayerPrefs.GetInt("GameDuration", 3);
		state = State.Waiting;
		gameplayTime = 0f;
	}

  private void Update() {
		switch (state) {
			case State.Waiting:
				waitingToStartTimer -= Time.deltaTime;
				if (waitingToStartTimer < 0f) {
					UIManager.Instance.StartCountdown();
					state = State.Countdown;
				}
				break;
			case State.Countdown:
				countdownToStartTimer -= Time.deltaTime;
				UIManager.Instance.UpdateCountdown(countdownToStartTimer);

				if (countdownToStartTimer < 0f) {
					ball.SetBallPosition();
					UIManager.Instance.StopCountdown();
					state = State.Playing;
				}
				break;

			case State.Playing:
				gameplayTime += Time.deltaTime;
				if (gameplayTime > (GetGameDuration() * 60)) {
					state = State.GameOver;
				}
				break;

			case State.GameOver:
				ball.gameObject.SetActive(false);
				UIManager.Instance.GameOver(player.GetScore(), ai.GetScore());
				break;

		}
  }

	public bool IsPlaying() {
		return state == State.Playing;
	}

	public float GetClockTimeNormalized() {
		return gameplayTime / (GetGameDuration() * 60);
	}

	public int GetGameDuration() {
		return gameplayTimerMax;
	}

	public void ScoreAGoal(PlayerBase playerObj) {
		ball.ResetBallPosition();
		playerObj.IncreaseScore();
		goalParticleSystem.Play();

		UIManager.Instance.UpdateScore(player.GetScore(), ai.GetScore());
		SoundManager.Instance.PlayGoalSound(transform.position);
	}

	public void OnShoot(PlayerBase playerObj) {
		playerObj.Shoot();
		SoundManager.Instance.PlayShootSound(playerObj.transform.position);
	}
}
