using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
  public GameObject[] helixRings;
  public GameObject lastRing;
  public float ySpawn = 0;
  public float ringDistance = 5;
  public int numberOfRings;
  public Component[] helixPieces;
  private GameObject prefab;
  private int rndNum;

  private Color32[] safeColors = { new Color32(0, 0, 0, 1), new Color32(92, 46, 126, 1), new Color32(130, 148, 96, 1), new Color32(225, 132, 42, 1) };
  private Color32[] unSafeColors = { new Color32(255, 0, 0, 1), new Color32(0, 0, 0, 1), new Color32(103, 71, 71, 1), new Color32(205, 16, 77, 1) };
  private Color32[] helixColors = { new Color32(128, 128, 128, 1), new Color32(76, 103, 147, 1), new Color32(238, 238, 238, 1), new Color32(253, 132, 31, 1) };
  private Color32[] camColors = { new Color32(255, 255, 255, 1), new Color32(139, 188, 204, 1), new Color32(249, 102, 102, 1), new Color32(156, 44, 119, 1) };

  void Start()
  {
    numberOfRings = GameManager.currentLevelIndex + 5;
    rndNum = Random.Range(0, camColors.Length - 1);

    // spawn helix rings
    for (int i = 0; i < numberOfRings; i++)
    {
      if (i == 0)
      {
        SpawnRing(0);
      }
      else
      {
        SpawnRing(Random.Range(1, helixRings.Length - 1));
      }

    }
    // Spawn last ring
    SpawnRing(helixRings.Length - 1);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SpawnRing(int ringIndex)
  {
    prefab = Instantiate(helixRings[ringIndex], ySpawn * transform.up, Quaternion.identity);

    ChangeTheme(prefab);
    prefab.transform.parent = transform;
    ySpawn -= ringDistance;
  }

  public void ChangeTheme(GameObject prefab)
  {

    GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
    MeshRenderer helix = prefab.GetComponentInChildren<MeshRenderer>();
    helixPieces = helix.GetComponentsInChildren<MeshRenderer>();

    camera.GetComponent<Camera>().backgroundColor = camColors[rndNum];
    helix.material.color = helixColors[rndNum];

    foreach (MeshRenderer mr in helixPieces)
    {
      if (mr.material.name == "Safe (Instance)")
      {
        mr.material.color = safeColors[rndNum];
      }
      else if (mr.material.name == "UnSafe (Instance)")
      {
        mr.material.color = unSafeColors[rndNum];
      }
    }
  }
}
