using System.Collections;
using PennBoy;
using UnityEngine;
using UnityEngine.UI;

public class HomePageManager : MonoBehaviour
{
    [SerializeField] private RawImage background;
    [SerializeField] private CanvasGroup overlay;
    [SerializeField] private GameNameScroller scroller;
    [SerializeField] private GameObject gamesList;

    private void Awake() {
        background.color = Theme.Up[9];
        overlay.alpha = 1f;

        // This way we don't have to manually set it for however many games we have...
        foreach (Transform childTransform in gamesList.transform) {
            childTransform.gameObject.GetComponent<GameChannel>().scroller = scroller;
        }
    }

    private IEnumerator Start() {
        yield return Anim.Animate(1f, t => {
            overlay.alpha = 1 - t;
        });
    }
}