using UnityEngine;

public class ClickedNoteFeedback : MonoBehaviour
{
    private bool noteEnabled = false;
    private float elapsedTime;

    private Vector2 noteTargetPos;
    private Vector2 noteStartPos;
    private Vector2 controlPoint;

    [SerializeField] private float lerpTime = 0.50f;
    [SerializeField] private float arch = 6f;

    public void EnableNote(Vector2 offset)
    {
        noteStartPos = transform.position;
        noteTargetPos = noteStartPos + offset;

        Vector2 midPoint = (noteStartPos + noteTargetPos) / 2f;
        controlPoint = midPoint + Vector2.up * arch;

        elapsedTime = 0f;
        noteEnabled = true;
    }

    private void Update()
    {
        if (!noteEnabled) return;

        elapsedTime += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(elapsedTime / lerpTime);

        Vector2 m1 = Vector2.Lerp(noteStartPos, controlPoint, t);
        Vector2 m2 = Vector2.Lerp(controlPoint, noteTargetPos, t);
        Vector2 bezierPoint = Vector2.Lerp(m1, m2, t);

        transform.position = bezierPoint;

        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
