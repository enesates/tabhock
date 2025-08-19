using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : PlayerBase {
  private Vector2 moveInput = Vector2.zero;

  public void OnMove(InputValue value) {
    if (GameManager.Instance.IsPlaying()) {
      moveInput = value.Get<Vector2>();
    }
  }

  private void Update() {
    if (moveInput.sqrMagnitude > 1) {
      moveInput.Normalize();
    }

    Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
    Vector3 rotated = Quaternion.Euler(0, -90, 0) * move;
    transform.position += rotated;
  }

  public override void Shoot() {
  }
}
