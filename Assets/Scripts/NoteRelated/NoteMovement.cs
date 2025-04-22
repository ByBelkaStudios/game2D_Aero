using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoteMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myrigidbody2d;

    private float timeToReachEnd;
    private float timeElapsed;

    private bool noteEnabled = false;
    private bool reachedTarget = false;

    private Vector2 startPos;
    private Vector2 targetPos;
    private Vector2 moveDirection;

    public void Enable(Vector2 spawnLocation, Vector2 targetLocation, float bpm, float songOffset)
    {
        timeToReachEnd = 60 / bpm;
        timeElapsed = songOffset;

        startPos = spawnLocation;
        targetPos = targetLocation;

        moveDirection = (targetPos - startPos).normalized;

        noteEnabled = true;
    }

    private void Update()
    {
        if (noteEnabled)
        {
            if (!reachedTarget)
            {
                float percentageComplete = Mathf.Clamp01(timeElapsed / timeToReachEnd);
                myrigidbody2d.position = Vector2.Lerp(startPos, targetPos, percentageComplete);

                if (percentageComplete >= 1)
                {
                    reachedTarget = true;
                    timeElapsed = 0f;

                    startPos = myrigidbody2d.position;
                    targetPos = startPos + moveDirection * 1.5f;
                    timeToReachEnd = timeToReachEnd / 4f;
                }

                Debug.Log(percentageComplete);
            }
            else
            {
                float percentageComplete = Mathf.Clamp01(timeElapsed / timeToReachEnd);
                myrigidbody2d.position = Vector2.Lerp(startPos, targetPos, percentageComplete);
            }

            timeElapsed += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
