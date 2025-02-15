using UnityEngine;

public class GameChannel : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void OpenGame() {
        Debug.Log($"Opening game with scene: {sceneName}");
    }
}