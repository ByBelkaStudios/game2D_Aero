using UnityEngine;

public class PulseController : MonoBehaviour
{
    public enum EpulseType
    {
        UI,
        GameObject
    };

    [SerializeField] public EpulseType pulseType;

    [SerializeField] private bool pulseOnBeat;

    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private float pulseDecayScale = 5.0f;
    [SerializeField] private float pulseScale = 1.05f;
    [SerializeField] private float maximumPulseScale = 2.0f;

    private Vector3 startingScale;

    private Transform gameObjectTransform;


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
        if(pulseType == EpulseType.UI)
        {
            startingScale = rectTransform.localScale;
            return;
        }

        gameObjectTransform = gameObject.transform;
        startingScale = gameObjectTransform.localScale;
    }

    public void Pulse()
    {
        if(pulseType == EpulseType.UI)
        {
            rectTransform.localScale = rectTransform.localScale * pulseScale;
            if (rectTransform.localScale.x > maximumPulseScale)
            {
                rectTransform.localScale = new Vector3(maximumPulseScale, maximumPulseScale, maximumPulseScale);
            }
            return;
        }

        gameObjectTransform.localScale = gameObjectTransform.localScale * pulseScale;
        if(gameObjectTransform.localScale.x > maximumPulseScale)
        {
            gameObjectTransform.localScale = new Vector3(maximumPulseScale, maximumPulseScale, maximumPulseScale);
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
        if(pulseType == EpulseType.UI)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, startingScale, Time.deltaTime * pulseDecayScale);
            return;
        }

        gameObjectTransform.localScale = Vector3.Lerp(gameObjectTransform.localScale, startingScale, Time.deltaTime * pulseDecayScale);
    }
}