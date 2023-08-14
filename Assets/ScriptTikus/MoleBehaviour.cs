using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoleBehaviour : MonoBehaviour
{
    [Header("Feedback Settings")]
    public FeedBackManager feedbackManager;
    [SerializeField] private float perfectReactionTime = 0.5f;
    [SerializeField] private float goodReactionTime = 1.0f;
    [SerializeField] private float penaltyTime = 2.0f;
    public int moleIndex;

    [Header("Mole Appearance")]
    public SpriteRenderer moleSpriteRenderer;
    public Sprite normalMoleSprite;
    public Sprite redMoleSprite;
    public Color normalColor = Color.white;
    public Color redColor = Color.red;

    [Header("Mole Timing & Scoring")]
    public float moleduration = 0.1f;
    public float reactionTime = 2.0f;
    public int latePenalty = -2;
    public int scoreValue;
    public int plusScore;
    public int minusScore;
    public float ratered = 0.2f;

    [Header("Mole State")]
    public bool isShown = false;
    public bool isRedMole;
    private bool wasClicked = false;
    private float redMoleTimer = 0f;
    private float whiteMoleTimer = 0f;

    public void ShowMole()
    {
        redMoleTimer = 0f;
        whiteMoleTimer = 0f;
        isShown = true;
        wasClicked = false;
        isRedMole = Random.value > ratered;

        gameObject.SetActive(true);
        if (isRedMole)
        {
            moleSpriteRenderer.sprite = redMoleSprite;
            scoreValue = minusScore;
            StartCoroutine(RedMoleTimer());
        }
        else
        {
            moleSpriteRenderer.sprite = normalMoleSprite;
            scoreValue = plusScore;
            StartCoroutine(CheckLateReaction());
        }
    }

    private void Update()
    {
        if (isRedMole && isShown)
        {
            redMoleTimer += Time.deltaTime;
        }
        else if (!isRedMole && isShown)
        {
            whiteMoleTimer += Time.deltaTime;
        }
    }

    public void HideMole()
    {
        isShown = false;
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (isShown)
        {
            wasClicked = true;
            float elapsedTime = (isRedMole ? redMoleTimer : whiteMoleTimer);
            string feedback = "";

            GameController.instance.MoleWasHit();
            if (isRedMole)
            {
                GameController.instance.RedMoleWasHit();
                feedback = ":(";
                scoreValue -= 3;
            }
            else
            {
                if (elapsedTime <= perfectReactionTime)
                {
                    feedback = "Perfect!";
                    scoreValue += 10;
                }
                else if (elapsedTime <= goodReactionTime)
                {
                    feedback = "Good!";
                    scoreValue += 5;
                }
                else if (elapsedTime <= penaltyTime)
                {
                    feedback = "Not Bad!";
                    scoreValue += 2;
                }
            }
            GameController.instance.UpdateScore(scoreValue);

            if (!string.IsNullOrEmpty(feedback))
            {
                feedbackManager.ShowFeedback(feedback, moleIndex, this.transform.position);
            }

            HideMole();
        }
    }

    IEnumerator HideMoleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideMole();
    }

    IEnumerator RedMoleTimer()
    {
        yield return new WaitForSeconds(reactionTime);
        if (isShown && isRedMole)
        {
            HideMole();
        }
    }

    IEnumerator CheckLateReaction()
    {
        yield return new WaitForSeconds(penaltyTime);
        if (isShown && !wasClicked && !isRedMole) // tambahkan pengecekan untuk mole yang bukan merah
        {
            feedbackManager.ShowFeedback(":)", moleIndex, this.transform.position);
            // Proses skor untuk tidak mengklik mole putih dalam waktu yang ditentukan
            HideMole();
        }
    }

    private void OnDestroy()
    {
        Debug.Log("molebehavior objek dihancurkan");
    }

    private void OnDisable()
    {
        Debug.Log("molebehavior dinonaktifkan");
    }

}
