using System.Collections;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myrigidbody2d;

    private float timeToReachEnd;
    private float timeElapsed;

    private bool noteEnabled = false;

    private Vector2 startPos;
    private Vector2 targetPos;

    public void Enable(Vector2 spawnLocation, Vector2 targetLocation, float bpm, float songOffset)
    {
        timeToReachEnd = 60 / bpm;
        timeElapsed += songOffset;

        startPos = spawnLocation;
        targetPos = targetLocation;

        noteEnabled = true;
    }

    private void Update()
    {
        if (noteEnabled)
        {
            timeElapsed += Time.deltaTime;

            float percentageComplete = Mathf.Clamp01(timeElapsed / timeToReachEnd);
            myrigidbody2d.position = Vector2.Lerp(startPos, targetPos, percentageComplete);

            //after it reaches the right place the note has to keep moving
            if (percentageComplete >= 1)
            {
                StartCoroutine(DestroySelf());
            }
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.10f);
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
