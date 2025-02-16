using UnityEngine;
using UnityEngine.UI;

public class EndlessBackground : MonoBehaviour
{
    [SerializeField] private RawImage background;

    // Update is called once per frame
    private void Update() {
        background.uvRect = new Rect(background.uvRect.x + Time.deltaTime * 0.04f, background.uvRect.y,
                                     background.uvRect.width, background.uvRect.height);
    }
}