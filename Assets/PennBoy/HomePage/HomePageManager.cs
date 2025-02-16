using System;
using System.Collections;
using PennBoy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomePageManager : MonoBehaviour
{
    [SerializeField] private RawImage background;
    [SerializeField] private CanvasGroup overlay;
    [SerializeField] private GameNameScroller scroller;
    [SerializeField] private GameObject gamesList;
    [SerializeField] private GameObject date;
    [SerializeField] private GameObject time;

    // Needs to be greater than the total time of FadeTo()
    private const float TIMER_LENGTH = 5f;

    private CanvasGroup dateCG;
    private CanvasGroup timeCG;
    private TMP_Text timeText;
    private float timerElapsed;

    private void Awake() {
        overlay.alpha = 1f;

        var now = DateTime.Now;
        date.GetComponent<TMP_Text>().text = $"{now:ddd} {now.Month}/{now.Day}";

        // This way we don't have to manually set it for however many games we have...
        foreach (Transform childTransform in gamesList.transform) {
            childTransform.gameObject.GetComponent<GameChannel>().scroller = scroller;
        }

        dateCG = date.GetComponent<CanvasGroup>();
        timeCG = time.GetComponent<CanvasGroup>();
        timeText = time.GetComponent<TMP_Text>();
        timeText.text = $"{now:HH:mm}";
    }

    private IEnumerator Start() {
        yield return Anim.Animate(1f, t => {
            overlay.alpha = 1 - t;
        });
    }

    private void Update() {
        timeText.text = $"{DateTime.Now:HH:mm}";

        timerElapsed += Time.deltaTime;
        if (timerElapsed >= TIMER_LENGTH) {
            StartCoroutine(FadeTo(dateCG.alpha == 0f));
            timerElapsed = 0f;
        }
    }

    private IEnumerator FadeTo(bool isDate) {
        var exit = isDate ? timeCG : dateCG;
        var enter = isDate ? dateCG : timeCG;

        yield return Anim.Animate(0.12f, t => {
            exit.alpha = 1 - t;
        });
        yield return Anim.Animate(0.12f, t => {
            enter.alpha = t;
        });
    }
}