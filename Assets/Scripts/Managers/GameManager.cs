using System;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UIManager UImanager;


    [SerializeField] private AudioSource noteSuccessSound;
    [SerializeField] private AudioSource noteMissSound;

    [SerializeField] private float decayScale = 5.0f;
    [SerializeField] private float pulseScale = 1.05f;
    [SerializeField] private float maximumScale = 2.0f;
    [SerializeField] private CinemachineImpulseSource listener;

    [SerializeField] private int score = 0;
    [SerializeField] private int combo = 0;
    [SerializeField] private float scoreMultiplier;

    public event Action OnUIUpdate;
    public event Action OnNoteMiss;

    public AudioSource SuccessNoteSound => noteSuccessSound;
    public AudioSource MissNoteSound => noteMissSound;

    public float DecayScale { get => decayScale; set => decayScale = value; }

    public float Multiplier { get => scoreMultiplier; set => scoreMultiplier = value; }
    public int Combo { get => combo; set => combo = value; }
    public int ScoreScale { get => scoreScale; set => scoreScale = value; }
    public int Score {  get => score; set => score = value;}


    [SerializeField] private float scoreMultiplierIncrease = 0.25f;
    [SerializeField] private float scoreMultiplerDefault = 1.00f;
    [SerializeField] private int scoreScale = 4;

    private void Awake()
    { 
        Instance = this;
    }

    public void ApplyImpulse()
    {
        listener.GenerateImpulse();
    }

    public void IncrementScore(int hit)
    {
        score = score + (int)MathF.Round(hit * Multiplier * ScoreScale);
        OnUIUpdate?.Invoke();
    }

    public void ResetCombo()
    {
        combo = 0;
        scoreMultiplier = scoreMultiplerDefault;
        OnUIUpdate?.Invoke();
    }

    public void IncrementCombo() { combo++; OnUIUpdate?.Invoke(); }
    public void IncrementMultiplier() { scoreMultiplier += scoreMultiplierIncrease; }

}
