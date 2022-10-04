
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Transform player;
  private Vector3 offset;
  public float smoothSpeed = 0.04f;

  void Update()
  {
    if (player.position.y < transform.position.y)
    {
      transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
    }
  }
}
