using System.Collections;
using System.Text;
using PennBoy;
using TMPro;
using UnityEngine;

public class GameNameScroller : MonoBehaviour
{
    public bool IsCurrentlyOpen { get; private set; }

    private float TimerLength {
        get {
            // A scroll speed of 100 approximately needs the timer to be 1.5 seconds long
            var ratio = 100f / scrollSpeed;
            return 1.5f * ratio;
        }
    }

    [SerializeField] private RectTransform bar;
    [SerializeField] private GameObject nameTextObj;
    [SerializeField] private float scrollSpeed;

    private const float Y_POS_OPEN = 306f;
    private const float Y_POS_CLOSED = 200f;
    private const float DURATION_APPEAR = 0.6f;
    private const float DURATION_CLOSE = 0.7f;

    private Coroutine curr;
    private StringBuilder sb;
    private string format;
    private TMP_Text gameName;
    private RectTransform rt;
    private float timerElapsed;

    private void Awake() {
        sb = new StringBuilder();
        gameName = nameTextObj.GetComponent<TMP_Text>();
        rt = nameTextObj.GetComponent<RectTransform>();

        // Hide bar again because it's visible initially in the editor
        Reset();
    }

    private void Update() {
        if (!IsCurrentlyOpen) return;

        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x - Time.deltaTime * scrollSpeed, rt.anchoredPosition.y);

        timerElapsed += Time.deltaTime;
        if (timerElapsed >= TimerLength) {
            sb.Append(format);
            gameName.text = sb.ToString();
            timerElapsed = 0f;
        }
    }

    private IEnumerator _Appear() {
        IsCurrentlyOpen = true;

        var elapsed = 0f;
        var init = new Vector2(bar.anchoredPosition.x, Y_POS_CLOSED);
        var final = new Vector2(bar.anchoredPosition.x, Y_POS_OPEN);

        while (elapsed < DURATION_APPEAR) {
            bar.anchoredPosition = Vector2.Lerp(init, final, Easing.EaseOutExpo(elapsed / DURATION_APPEAR));
            elapsed += Time.deltaTime;
            yield return null;
        }

        bar.anchoredPosition = final;
    }

    private IEnumerator _Disappear() {
        if (curr != null) StopCoroutine(curr);

        var elapsed = 0f;
        var init = bar.anchoredPosition;
        var final = new Vector2(init.x, Y_POS_CLOSED);

        while (elapsed < DURATION_CLOSE) {
            bar.anchoredPosition = Vector2.Lerp(init, final, Easing.EaseOutExpo(elapsed / DURATION_CLOSE));
            elapsed += Time.deltaTime;
            yield return null;
        }

        bar.anchoredPosition = final;
        rt.anchoredPosition = new Vector2(0f, rt.anchoredPosition.y);
        IsCurrentlyOpen = false;
    }

    public void Appear() => curr = StartCoroutine(_Appear());
    public void Disappear() => curr = StartCoroutine(_Disappear());

    public void UpdateText(string newGameName) {
        sb.Clear();
        format = $"{newGameName} / ";

        for (var i = 0; i < 9; i++) {
            sb.Append(format);
        }

        gameName.text = sb.ToString();
    }

    public void Reset() {
        if (curr != null) StopCoroutine(curr);
        bar.anchoredPosition = new Vector2(bar.anchoredPosition.x, Y_POS_CLOSED);
        rt.anchoredPosition = new Vector2(0f, rt.anchoredPosition.y);
        IsCurrentlyOpen = false;
    }
}