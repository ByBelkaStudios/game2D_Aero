using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UIManager UImanager;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private AudioSource noteSuccessSound;
    [SerializeField] private AudioSource noteMissSound;

    [SerializeField] private float decayScale = 5.0f;
    [SerializeField] private float pulseScale = 1.05f;
    [SerializeField] private float maximumScale = 2.0f;
    [SerializeField] private CinemachineImpulseSource listener;

    [SerializeField] private int score = 0;
    [SerializeField] private int combo = 0;
    [SerializeField] private float scoreMultiplier;
    [SerializeField] private float scoreMultiplierLimit;

    public event Action OnNoteSuccess;
    public event Action OnUIUpdate;
    public event Action OnNoteMiss;
    public event Action OnHealthChanged;

    public AudioSource SuccessNoteSound => noteSuccessSound;
    public AudioSource MissNoteSound => noteMissSound;

    public HealthBar HealthBar => healthBar;
    public float DecayScale { get => decayScale; set => decayScale = value; }
    public float PulseScale { get => pulseScale; set => pulseScale = value; }
    public float MaximumScale {get => maximumScale; set => maximumScale = value; }

    public float Multiplier { get => scoreMultiplier; set => scoreMultiplier = value; }
    public int Combo { get => combo; set => combo = value; }
    public int ScoreScale { get => scoreScale; set => scoreScale = value; }
    public int Score {  get => score; set => score = value;}

    [SerializeField] private float scoreMultiplierIncrease = 0.25f;
    [SerializeField] private float scoreMultiplerDefault = 1.00f;
    [SerializeField] private int scoreScale = 4;

    public SongData songdata;
    public SongData SelectSongData { get => songdata; set => songdata = value; }

    private void Start()
    {
        scoreMultiplier = scoreMultiplerDefault;
    }

    private void Awake()
    { 
        Instance = this;
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    public SongData LoadSong()
    {
        return songdata;
    }

    private System.Collections.IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ApplyImpulse()
    {
        listener.GenerateImpulse();
    }

    public void ApplyPulse()
    {
        OnNoteSuccess?.Invoke();
    }
    
    public void InvokeOnHealthChange()
    {
        OnHealthChanged?.Invoke();
    }

    public void IncrementScore(int hit)
    {
        score = score + (int)MathF.Round(hit * Multiplier * ScoreScale);
        PlayerPrefs.SetString("score", $"Last Score {score}");
        OnUIUpdate?.Invoke();
    }

    public void ResetCombo()
    {
        combo = 0;
        scoreMultiplier = scoreMultiplerDefault;
        OnUIUpdate?.Invoke();
    }

    public void IncrementCombo() { combo++; OnUIUpdate?.Invoke(); }

    public void IncrementMultiplier()
    {
        if(scoreMultiplier >= scoreMultiplierLimit)
        {
            return;
        }
        scoreMultiplier += scoreMultiplierIncrease;
    }

}
