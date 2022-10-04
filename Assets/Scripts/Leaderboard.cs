using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
  int leaderboardID = 7462;
  public TextMeshProUGUI playerNames;
  public TextMeshProUGUI playerScores;
  public GameObject leaderboardPanel;

  public void GetTopScores()
  {
    leaderboardPanel.SetActive(true);
    LootLockerSDKManager.GetScoreList(leaderboardID, 10, 0, (response) =>
    {
      if (response.success)
      {
        string tempPlayerNames = "Names\n";
        string tempPlayerScores = "Scores\n";
        LootLockerLeaderboardMember[] members = response.items;
        for (int i = 0; i < members.Length; i++)
        {
          tempPlayerNames += members[i].rank + "- ";
          if (members[i].player.name != "")
          {
            tempPlayerNames += members[i].player.name;
          }
          else
          {
            tempPlayerNames += members[i].player.id;
          }
          tempPlayerScores += members[i].score + "\n";
          tempPlayerNames += "\n";
        }
        playerNames.text = tempPlayerNames;
        playerScores.text = tempPlayerScores;
      }
      else
      {
        Debug.Log("Failed " + response.Error);
      }
    });
  }
}
