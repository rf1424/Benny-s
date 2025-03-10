using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private static Pause I;
    
    [SerializeField] private GameObject pause;
    [SerializeField] private CanvasGroup overlay;
    
    private float timeScale;
    private CursorLockMode prevLockState;
    private bool isPaused;
    
    public string[] lockedScenes;
    
    public void TogglePauseGame() {
        // not paused
        timeScale = Time.timeScale != 0f ? Time.timeScale : timeScale;
        prevLockState = Cursor.lockState == CursorLockMode.None ? prevLockState : Cursor.lockState;

        isPaused = !isPaused;
        pause.SetActive(isPaused);

        Cursor.lockState = isPaused ? CursorLockMode.None : prevLockState;
        Cursor.visible = Cursor.lockState == CursorLockMode.None;

        Time.timeScale = isPaused ? 0f : timeScale; // true means is paused now
        Debug.Log("Timescale is now " + Time.timeScale);
        Debug.Log("LockState: " + Cursor.lockState);
    }

    public void ResumeGame() {
        Debug.Log("Resume hit");
        isPaused = true;
        TogglePauseGame();
    }

    public void ReturnHome() {
        Cursor.lockState = CursorLockMode.None;
        prevLockState = CursorLockMode.None;
        ResumeGame();

        // Let the PulseTransition scene take us back home
        SceneManager.LoadScene("PulseTransition");
    }

    private bool CheckIfBad() {
        //  This can be optimized if we cache by current scene name, but since we have so few locked scenes it doesn't matter
        var sceneName = SceneManager.GetActiveScene().name;

        foreach (var locked in lockedScenes) {
            if (locked == sceneName) {
                return true;
            }
        }

        return false;
    }

    private void Awake() {
        if (I == null) {
            // default behavior: pause = isPaused
            I = this;
            timeScale = Time.timeScale;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !CheckIfBad()) {
            TogglePauseGame();
        }
    }
}