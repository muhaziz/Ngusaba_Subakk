using UnityEngine;

public class NotehitDetector : MonoBehaviour
{
    public KeyCode associatedKey;
    private NoteBehavior currentNote;
    // public Vector2 lastKeyPressedPosition;  // Menyimpan posisi terakhir trigger ditekan

    public int missPenalty = -1;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            currentNote = other.GetComponent<NoteBehavior>();
            currentNote.timeEnteredTrigger = Time.time;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            if (!currentNote.wasClicked)
            {
                currentNote.ShowFeedback("Miss!", missPenalty);

            }
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {


        if (currentNote && Input.GetKeyDown(associatedKey))
        {


            currentNote.ProcessHit();
            Destroy(currentNote.gameObject);
            float reactionTime = Time.time - currentNote.timeEnteredTrigger;

            if (reactionTime < 0.2f)
            {
                currentNote.ShowFeedback("Perfect!", 100);
            }
            else if (reactionTime < 0.5f)
            {
                currentNote.ShowFeedback("Good!", 70);
            }
            else if (reactionTime < 1f)
            {
                currentNote.ShowFeedback("Not Bad!", 50);
            }

            currentNote.wasClicked = true;
            Destroy(currentNote.gameObject);
        }
    }
}
