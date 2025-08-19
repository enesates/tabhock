using UnityEngine;

public class BallController : MonoBehaviour {
  [SerializeField] private float collisionForce = 25f;

  private Rigidbody rb;
  private Vector3 firstLocation;
  private float hideBallTimer = 2f;
  private float ballOutPosX = 6.5f;
  private float ballOutPosZ = 10f;

  private void Start() {
    rb = GetComponent<Rigidbody>();
  }

  private void Update() {
    if (transform.position.x < -1 * ballOutPosX || transform.position.x > ballOutPosX || transform.position.z < -1 * ballOutPosZ || transform.position.z > ballOutPosZ) {
      ResetBallPosition();
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.TryGetComponent<PlayerBase>(out var player)) {
      Vector3 forceDirection = collision.contacts[0].point - collision.gameObject.transform.position;
      forceDirection = forceDirection.normalized;
      rb.AddForce(forceDirection * collisionForce, ForceMode.Impulse);

      GameManager.Instance.OnShoot(player);
    }
  }

  public void ResetBallPosition() {
    transform.gameObject.SetActive(false);
    rb.linearVelocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    Invoke(nameof(SetBallPosition), hideBallTimer);
  }

  public void SetBallPosition() {
    transform.position = new Vector3(0f, 0.5f, 0);
    transform.gameObject.SetActive(true);
    firstLocation = new Vector3(Random.Range(-6, 6), transform.position.y, Random.Range(-9, 9));

    rb.AddForce(firstLocation * 2f, ForceMode.Impulse);
  }
}
