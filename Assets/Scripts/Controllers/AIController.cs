using UnityEngine;

public class AIController : PlayerBase {  
  [SerializeField] private BallController ball;
  private bool farEnough = false;

  public override void Shoot() {
    farEnough = false;
  }

  private void Update() {
    if (GameManager.Instance.IsPlaying()) {
      float ballPositionX = ball.transform.position.x;
      float ballPositionZ = ball.transform.position.z;
      float x;
      float z;

      if (transform.position.z < ballPositionZ) {
        x = (transform.position.x < ballPositionX) ? ballPositionX - 1f : ballPositionX + 1f;
        z = ballPositionZ + 3f;
      } else {
        if (ballPositionZ <= -0.5f) {
          x = ballPositionX / 2;
          z = 5f;
        } else {
          if (!farEnough) {
            x = ballPositionX + (transform.position.x < ballPositionX ? -0.5f : 0.5f);
            z = ballPositionZ + 3f;

            if (transform.position.z - ballPositionZ <= 3f) {
              farEnough = true;
            }
          } else {
            x = ballPositionX + ballPositionX * UnityEngine.Random.Range(0.06f, 0.08f);
            z = ballPositionZ + 0.5f;
          }
        }
      }

      if (transform.position.x > 5.5f || transform.position.x < -5.5f || transform.position.z > 9f || transform.position.z < -9f) {
        z = ballPositionZ;
        x = ballPositionX;
      }

      transform.position = Vector3.MoveTowards(
        transform.position,
        new Vector3(x, transform.position.y, z),
        moveSpeed * Time.deltaTime
      );
    }
  }
}
