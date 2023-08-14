using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationText : MonoBehaviour
{

    public TextMeshProUGUI feedbackText;
    public float waitTime = 1.0f;  // Waktu menunggu sebelum fade out setelah fade in

    public void PlayFadeIn()
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        yield return FadeIn();
        yield return new WaitForSeconds(waitTime);
        yield return FadeOut();
    }

    IEnumerator FadeIn()
    {
        float duration = 0.15f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            feedbackText.color = new Color(feedbackText.color.r, feedbackText.color.g, feedbackText.color.b, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float duration = 0.15f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            feedbackText.color = new Color(feedbackText.color.r, feedbackText.color.g, feedbackText.color.b, alpha);
            yield return null;
        }
    }
}
