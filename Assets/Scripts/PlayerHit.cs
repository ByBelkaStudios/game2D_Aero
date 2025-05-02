using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnNoteMiss += OnPlayerHit;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnNoteMiss -= OnPlayerHit;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnNoteMiss -= OnPlayerHit;
    }

    private void OnPlayerHit()
    {
        GameManager.Instance.ApplyImpulse();
        GameManager.Instance.ResetCombo();

        GameManager.Instance.MissNoteSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPlayerHit();
    }
}
