
using UnityEngine;
using TMPro;

public class Ring : MonoBehaviour
{
  private Transform player;

  public TextMeshProUGUI currentLevelText;


  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;

  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.y > player.position.y)
    {
      FindObjectOfType<AudioManager>().Play("whoosh");
      GameManager.numberOfpassedRings++;
      GameManager.score += 100;
      Destroy(gameObject);
    }
  }
}
