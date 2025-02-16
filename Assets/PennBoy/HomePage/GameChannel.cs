using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChannel : MonoBehaviour
{
    [SerializeField] private bool placeholder;
    [SerializeField] private string sceneName;
    [SerializeField] private Material staticMaterial;

    public void OpenGame() {
        if (placeholder) return;

        Debug.Log($"Opening game with scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}