using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimationControl : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;

    public void PlayFadeIn()
    {
        StartCoroutine(FadeIn());
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
}
