using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasScript : MonoBehaviour
{
    public static CanvasScript instance;
    [SerializeField] TextMeshProUGUI scoreText;
    GameObject gameOverScreen;

    private int testIterator = 1;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gameOverScreen = GameObject.Find("GameOverScreen");
        gameOverScreen.SetActive(false);
    }
    // Call this method to restart the current scene.
    // Currently used by the restart button in the game over scene.
    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Call this with the new score to display an updated score
    public void UpdateScore(int newScore) {
        scoreText.text = "Score:\n" + newScore.ToString();
    }

    public void ScoreTest() {
        UpdateScore(testIterator * 100);
        testIterator++;
    }

    // Call this from a game over function somewhere else that also
    // disables player controls (probably)
    public void DisplayGameOver() {
        gameOverScreen.SetActive(true);
    }
}
