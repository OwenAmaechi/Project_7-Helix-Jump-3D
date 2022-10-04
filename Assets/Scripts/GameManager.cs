using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using LootLocker.Requests;

public class GameManager : MonoBehaviour
{
  public Leaderboard leaderboard;
  public GameObject gameOverPanel;
  public GameObject levelCompletdPanel;
  public GameObject gamePlayPanel;
  public GameObject startMenuPanel;
  public GameObject leaderboardPanel;
  public TextMeshProUGUI currentLevelText;
  public TextMeshProUGUI nextLevelText;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI highScoreText;
  public Slider gameProgressSlider;
  public static bool gameOver;
  public static bool isGameStarted;
  public static int currentLevelIndex;
  public static int numberOfpassedRings;
  public static int score = 0;

  public static bool levelCompleted;
  public static bool mute = false;


  private void Awake()
  {

    currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);

  }
  // Start is called before the first frame update
  void Start()
  {
    Time.timeScale = 1; // used to start the game
    numberOfpassedRings = 0;
    highScoreText.text = "Best Score\n" + PlayerPrefs.GetInt("HighScore", 0);
    isGameStarted = gameOver = levelCompleted = false;
  }

  // Update is called once per frame
  void Update()
  {
    // Update our UI
    currentLevelText.text = currentLevelIndex.ToString();
    nextLevelText.text = (currentLevelIndex + 1).ToString();

    int progress = numberOfpassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
    gameProgressSlider.value = progress;

    scoreText.text = score.ToString();

    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
    {
      if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
      isGameStarted = true;
      gamePlayPanel.SetActive(true);
      startMenuPanel.SetActive(false);
    }

    if (gameOver)
    {
      // leaderboard.SubmitScore(score);
      Time.timeScale = 0; // used to stop the game

      gameOverPanel.SetActive(true);

      if (Input.GetButtonDown("Fire1"))
      {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
          PlayerPrefs.SetInt("HighScore", score);
        }
        score = 0;
        SceneManager.LoadScene(0); // use the Scene name "Level"
      }
    }
    if (levelCompleted)
    {
      PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
      levelCompletdPanel.SetActive(true);
      if (Input.GetButtonDown("Fire1"))
      {
        SceneManager.LoadScene(0); // use the Scene name "Level"
      }
    }
  }
}
