using System.Collections;
using TMPro;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{

    public AnimationText textAnimController;
    public TextMeshProUGUI feedbackText;
    public float feedbackDuration = 0.2f;
    public float noteDuration = 0.1f;
    public bool isShown = false;
    public bool isRedNote;
    public SpriteRenderer noteSpriteRenderer;
    public Sprite normalNoteSprite;
    public Sprite redNoteSprite;
    public float moveSpeed = 5f;
    public int missPenalty = -1;
    public bool wasClicked = false;
    string namaObject = "feedback";
    public float timeEnteredTrigger;

    private Coroutine hideFeedbackCoroutine;
    private void Start()
    {
        textAnimController = FindObjectOfType<AnimationText>();

        GameObject obj = GameObject.Find(namaObject);
        if (obj != null)
        {
            feedbackText = obj.GetComponent<TextMeshProUGUI>();
        }

    }
    public void ShowNote()
    {
        Debug.Log("ShowNote Called");

        isShown = true;
        wasClicked = false;

        isRedNote = Random.value < 1f;

        if (isRedNote)
        {
            noteSpriteRenderer.sprite = redNoteSprite;
            Debug.Log("Generated Red Note");
        }
        else
        {
            noteSpriteRenderer.sprite = normalNoteSprite;
            Debug.Log("Generated White Note");
        }

        gameObject.SetActive(true);
    }


    private void Update()
    {
        transform.position += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
    }
    public void ProcessHit()
    {
        if (isShown && !wasClicked)
        {

            float reactionTime = Time.time - timeEnteredTrigger;

            if (reactionTime < 0.2f)
            {
                feedbackText.text = "Perfect!";
                GameRythemControl.instance.UpdateScore(100);
            }
            else if (reactionTime < 0.5f)
            {
                feedbackText.text = "Good!";
                GameRythemControl.instance.UpdateScore(70);
            }
            else if (reactionTime < 1f)
            {
                feedbackText.text = "Not Bad!";
                GameRythemControl.instance.UpdateScore(50);
            }

            feedbackText.gameObject.SetActive(true);
            textAnimController.PlayFadeIn();

            if (hideFeedbackCoroutine != null)
            {
                StopCoroutine(hideFeedbackCoroutine);
            }
            hideFeedbackCoroutine = StartCoroutine(HideFeedbackAfterDelay(feedbackDuration));

            wasClicked = true;
        }
    }

    IEnumerator HideFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        feedbackText.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!wasClicked)
        {
            Debug.Log("Miss!");
            GameRythemControl.instance.UpdateScore(missPenalty);
            feedbackText.text = "Miss!";
            feedbackText.gameObject.SetActive(true);
        }
        Destroy(gameObject);
    }
    public void ShowFeedback(string feedback, int score)
    {
        feedbackText.text = feedback;
        feedbackText.gameObject.SetActive(true);
        GameRythemControl.instance.UpdateScore(score);

        textAnimController.PlayFadeIn();
    }
}
