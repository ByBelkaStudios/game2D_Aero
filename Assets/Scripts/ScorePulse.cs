using UnityEngine;

public class ScorePulse : MonoBehaviour
{
    [SerializeField] private Vector3 startingScale;
    [SerializeField] private RectTransform RectTransform;
 
    private void Start()
    {
        startingScale = RectTransform.localScale;
    }

    public void Pulse()
    {
        RectTransform.localScale = RectTransform.localScale * GameManager.Instance.PulseScale;
        if (RectTransform.localScale.x > GameManager.Instance.MaximumScale)
        {
            RectTransform.localScale = new Vector3(GameManager.Instance.MaximumScale, GameManager.Instance.MaximumScale, GameManager.Instance.MaximumScale);
        }
    }

    private void Update()
    {
        RectTransform.localScale = Vector3.Lerp(RectTransform.localScale, startingScale, Time.deltaTime * GameManager.Instance.DecayScale);
    }
}
