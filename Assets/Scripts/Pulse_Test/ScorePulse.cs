using UnityEngine;

public class ScorePulse : MonoBehaviour
{
    [SerializeField] private bool isUI;
    [SerializeField] private bool pulseOnBeat;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform gameObjectTransform;

    [SerializeField] private Vector3 startingScale;

    private void OnEnable()
    {
        GameManager.Instance.OnSongBeat += BeatPulse;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnSongBeat -= BeatPulse;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSongBeat -= BeatPulse;

    }

    private void Start()
    {
        if(isUI)
        {
            startingScale = rectTransform.localScale;
            return;
        }

        gameObjectTransform = gameObject.transform;
        startingScale = gameObjectTransform.localScale;
    }

    public void Pulse()
    {
        if(isUI)
        {
            rectTransform.localScale = rectTransform.localScale * GameManager.Instance.PulseScale;
            if (rectTransform.localScale.x > GameManager.Instance.MaximumScale)
            {
                rectTransform.localScale = new Vector3(GameManager.Instance.MaximumScale, GameManager.Instance.MaximumScale, GameManager.Instance.MaximumScale);
            }
            return;
        }

        gameObjectTransform.localScale = gameObjectTransform.localScale * GameManager.Instance.PulseScale;
        if(gameObjectTransform.localScale.x > GameManager.Instance.MaximumScale)
        {
            gameObjectTransform.localScale = new Vector3(GameManager.Instance.MaximumScale, GameManager.Instance.MaximumScale, GameManager.Instance.MaximumScale);
        }
    }

    public void BeatPulse()
    {
        if (pulseOnBeat)
        {
            Pulse();
        }
    }

    private void Update()
    {
        if(isUI)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, startingScale, Time.deltaTime * GameManager.Instance.DecayScale);
            return;
        }

        gameObjectTransform.localScale = Vector3.Lerp(gameObjectTransform.localScale, startingScale, Time.deltaTime * GameManager.Instance.DecayScale);
    }
}