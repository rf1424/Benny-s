using System.Collections;
using System.Text;
using PennBoy;
using TMPro;
using UnityEngine;

public class GameNameScroller : MonoBehaviour
{
    public bool IsCurrentlyOpen => curr != null;

    [SerializeField] private RectTransform bar;
    [SerializeField] private TMP_Text text;

    private const float Y_POS_OPEN = 306f;
    private const float Y_POS_CLOSED = 200f;
    private const float DURATION_APPEAR = 0.6f;
    private const float DURATION_CLOSE = 0.7f;

    private Coroutine curr;
    private StringBuilder sb;

    private void Awake() {
        // Hide bar again because it's visible initially in the editor
        Reset();
        sb = new StringBuilder();
    }

    private IEnumerator _Appear() {
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
    }

    public void Appear() => curr = StartCoroutine(_Appear());
    public void Disappear() => curr = StartCoroutine(_Disappear());

    public void UpdateText(string gameName) {
        sb.Clear();
        var format = $"{gameName} / ";

        for (var i = 0; i < 9; i++) {
            sb.Append(format);
        }

        text.text = sb.ToString();
    }

    public void Reset() {
        if (curr != null) StopCoroutine(curr);
        bar.anchoredPosition = new Vector2(bar.anchoredPosition.x, Y_POS_CLOSED);
    }
}