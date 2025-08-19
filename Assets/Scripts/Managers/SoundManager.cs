using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager> {
	[SerializeField] private AudioClip shootAudio;
	[SerializeField] private AudioClip goalAudio;
	[SerializeField] private float effectVolume = 1f;

	private void PlaySound(AudioClip audioClip, Vector3 position, float volume) {
		AudioSource.PlayClipAtPoint(audioClip, position, volume);
	}

	public void PlayShootSound(Vector3 position) {
		PlaySound(shootAudio, position, effectVolume);
	}

	public void PlayGoalSound(Vector3 position) {
		PlaySound(goalAudio, position, effectVolume);
	}
}
