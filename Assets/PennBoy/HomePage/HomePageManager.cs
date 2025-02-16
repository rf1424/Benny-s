using System.Collections;
using PennBoy;
using UnityEngine;
using UnityEngine.UI;

public class HomePageManager : MonoBehaviour
{
    [SerializeField] private RawImage background;
    [SerializeField] private CanvasGroup overlay;

    private void Awake() {
        background.color = Theme.Up[9];
        overlay.alpha = 1f;
    }

    private IEnumerator Start() {
        yield return Anim.Animate(1.6f, t => {
            overlay.alpha = 1 - t;
        });
    }
}