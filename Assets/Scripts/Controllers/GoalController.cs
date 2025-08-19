using UnityEngine;

public class GoalController : MonoBehaviour {
  [SerializeField] private PlayerBase player;

  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Balls")) {
      GameManager.Instance.ScoreAGoal(player);
    }
  }
}
