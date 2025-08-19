using UnityEngine;

public abstract class PlayerBase : MonoBehaviour {
  [SerializeField] protected float moveSpeed = 5f;

  private int score = 0;

  public void IncreaseScore() {
    score++;
  }

  public int GetScore() {
    return score;
  }

  public abstract void Shoot();
}
