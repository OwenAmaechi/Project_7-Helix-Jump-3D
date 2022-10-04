using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
public class Player : MonoBehaviour
{

  public Rigidbody playerRb;
  public Leaderboard leaderboard;
  private AudioManager audioManager;

  public float bounceForce = 6.0f;
  int leaderboardID = 7462;

  private void Start()
  {
    audioManager = FindObjectOfType<AudioManager>();
    StartCoroutine(LoginRoutine());
  }

  IEnumerator LoginRoutine()
  {
    bool done = false;
    LootLockerSDKManager.StartGuestSession((response) =>
    {
      if (response.success)
      {
        Debug.Log("Player was logged in");
        PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
        done = true;
      }
      else
      {
        // Debug.Log("Could not start session");
        done = true;
      }
    });
    yield return new WaitWhile(() => done == false);
  }
  public IEnumerator SubmitScoreRoutine(int scoreToUpload)
  {
    Debug.Log("inside submit score");
    bool done = false;
    string PlayerID = PlayerPrefs.GetString("PlayerID");
    LootLockerSDKManager.SubmitScore(PlayerID, scoreToUpload, leaderboardID, (response) =>
      {
        if (response.success)
        {
          Debug.Log("Succesfully uploaded the score");
          done = true;
        }
        else
        {
          Debug.Log("Failed " + response.Error);
          done = true;
        }
      });
    Debug.Log("outside submit score");
    yield return new WaitWhile(() => done == false);
  }

  private void OnCollisionEnter(Collision collision)
  {
    audioManager.Play("bounce");
    playerRb.velocity = new Vector3(playerRb.velocity.x, bounceForce, playerRb.velocity.z);
    string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;
    if (materialName == "Safe (Instance)")
    {
      // the ball hit the safe area

    }
    else if (materialName == "UnSafe (Instance)")
    {
      // the ball hit the unsafe area

      GameManager.gameOver = true;
      StartCoroutine(SubmitScoreRoutine(GameManager.score));
      audioManager.Play("game over");
    }
    else if (materialName == "LastRing (Instance)" && !GameManager.levelCompleted)
    {
      // completed the level
      GameManager.levelCompleted = true;
      audioManager.Play("win level");
    }
  }
}
