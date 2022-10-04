using UnityEngine;

public class Rotator : MonoBehaviour
{
  public float rotationSpeed = 0.01f;


  // Update is called once per frame
  void Update()
  {
    if (!GameManager.isGameStarted) return;
    // for PC
    // if (Input.GetMouseButton(0))
    // {
    //   float mouseX = Input.GetAxisRaw("Mouse X");
    //   transform.Rotate(0, -mouseX * rotationSpeed * Time.deltaTime, 0);
    // }

    // for Mobile
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
    {
      float xDelta = Input.GetTouch(0).deltaPosition.x / 130;
      transform.Rotate(Vector3.up, -xDelta * rotationSpeed * Time.deltaTime, 0);
    }
  }
}
