using System.Collections;
using PennBoy;
using UnityEngine;

public class HomePageManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup overlay;

    private void Awake() {
        overlay.alpha = 0f;
    }

    private IEnumerator Start() {
        yield return Anim.Animate(1f, t => {
            overlay.alpha = 1 - t;
        });
    }
}