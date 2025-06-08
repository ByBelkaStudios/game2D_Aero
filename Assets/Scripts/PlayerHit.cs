using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnPlayerHit()
    {
        GameManager.Instance.HealthBar.DecreaseHealth();

        GameManager.Instance.ApplyImpulse();
        GameManager.Instance.ResetCombo();

        GameManager.Instance.InvokeOnHitTaken();

        GameManager.Instance.MissNoteSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPlayerHit();
    }
}
