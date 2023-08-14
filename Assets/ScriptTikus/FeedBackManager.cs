using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    public static FeedBackManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    public List<TextMeshProUGUI> feedbackTexts;
    public float feedbackDuration = 0.2f;

    public float fadeInDuration = 0.5f;
    public float stayDuration = 0.5f;
    public float fadeOutDuration = 0.5f;
    private List<Coroutine> activeAnimations = new List<Coroutine>();

    private Dictionary<TextMeshProUGUI, Coroutine> activeFeedbackCoroutines = new Dictionary<TextMeshProUGUI, Coroutine>();

    public void ShowFeedback(string feedback, int moleIndex, Vector3 molePosition)
    {
        TextMeshProUGUI text = feedbackTexts[moleIndex];
        text.text = feedback;
        text.gameObject.SetActive(true);

        // Efek pergeseran dinamis lebih sedikit
        text.rectTransform.position = molePosition + RandomOffset(1.0f); // Mengurangi magnitude menjadi 5.0f untuk perpindahan yang lebih sedikit

        // Efek rotasi acak
        text.rectTransform.rotation = Quaternion.Euler(0, 0, Random.Range(-15f, 15f));

        // Animasi pergerakan dari posisi mole
        Vector3 startPos = molePosition;
        StartCoroutine(MoveTextOverTime(text, startPos, text.rectTransform.position, 0.3f));

        // Memulai animasi
        Coroutine anim = StartCoroutine(TextAnimationCoroutine(text));
        activeAnimations.Add(anim);
    }

    private IEnumerator HideFeedbackAfterDelay(TextMeshProUGUI targetText, float delay)
    {
        yield return new WaitForSeconds(delay);
        targetText.gameObject.SetActive(false);
    }

    private IEnumerator TextAnimationCoroutine(TextMeshProUGUI feedbackText)
    {
        // Fade in
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
            feedbackText.color = new Color(feedbackText.color.r, feedbackText.color.g, feedbackText.color.b, alpha);
            yield return null;
        }

        // Tetap muncul selama stayDuration
        yield return new WaitForSeconds(stayDuration);

        // Fade out
        elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            feedbackText.color = new Color(feedbackText.color.r, feedbackText.color.g, feedbackText.color.b, alpha);
            yield return null;
        }

        feedbackText.gameObject.SetActive(false);
    }


    Vector3 RandomOffset(float magnitude)
    {
        return new Vector3(Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude), 0);
    }

    private IEnumerator MoveTextOverTime(TextMeshProUGUI text, Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            text.rectTransform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        text.rectTransform.position = endPos;
    }
}
